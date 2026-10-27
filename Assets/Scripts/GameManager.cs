using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gmInstance;

    private ShipData selectedShipData1;
    private ShipData selectedShipData2;

    public enum MatchState {Waiting, RoundCountdown, RoundInPlay, RoundOver, MatchOver}
    private int roundNumber = 0;
    private int p1Wins = 0;
    private int p2Wins = 0;
    private MatchState currentGameState;
    private int roundTimer;
    private Coroutine timerCoroutine;
    private HUDController hud;
    bool gamePaused = false;

    private void Awake()
    {
        if (gmInstance == null)
        {
            gmInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }    
    }

    private void Start()
    {
        AudioManager.amInstance.PlayMusic("GameTheme");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && currentGameState == MatchState.RoundInPlay)
        {

            PlayerInput p1Input = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerInput>();
            PlayerInput p2Input = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerInput>();

            if (gamePaused)
            {
                gamePaused = false;
                p1Input.ActivateInput();
                p2Input.ActivateInput();
                Time.timeScale = 1;
            }
            else
            {
                gamePaused = true;
                p1Input.DeactivateInput();
                p2Input.DeactivateInput();
                Time.timeScale = 0;
            }

            hud.SetPausePanel(gamePaused);
        }
    }
    public void TryMatchStart()
    {
        ShipSelectPanel[] panels = FindObjectsByType<ShipSelectPanel>();
        Array.Sort(panels, (a, b) => a.playerIndex.CompareTo(b.playerIndex));

        foreach (ShipSelectPanel panel in panels)
        {
            if(!panel.GetReadyState())
            {
                return;
            }
        }
        Debug.Log("Both Players are ready.");

        selectedShipData1 = panels[0].GetCurrentShipData();
        selectedShipData2 = panels[1].GetCurrentShipData();

        p1Wins = 0;
        p2Wins = 0;
        roundNumber = 0;

        AudioManager.amInstance.Play("ButtonClick");
        AudioManager.amInstance.Stop("GameTheme");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        AudioManager.amInstance.PlayMusic("FightTheme");
        StartCoroutine(PreRound());
    }
    public void RegisterHUD(HUDController newHud)
    {
        hud = newHud;
        hud.SetWins(1, p1Wins);
        hud.SetWins(2, p2Wins);
    }

    IEnumerator PreRound()
    {
        roundTimer = 90;
        roundNumber++;
        currentGameState = MatchState.RoundCountdown;

        yield return null;
        SetPlayerInput(false);

        GameObject.FindGameObjectWithTag("Player1").GetComponent<SpriteRenderer>().color = Color.white;
        GameObject.FindGameObjectWithTag("Player2").GetComponent<SpriteRenderer>().color = Color.white;

        Coroutine preroundTimerCoroutine = StartCoroutine(hud.RunPreroundTimer(3));
        AudioManager.amInstance.Play("PreRoundCountdown");    
        yield return new WaitForSeconds(4f);
        SetPlayerInput(true);

        StartRound();
    }

    public void StartRound()
    {
        if(currentGameState == MatchState.RoundCountdown)
        {
            Debug.Log("Round Number: " + roundNumber + " started");
            currentGameState = MatchState.RoundInPlay;
            timerCoroutine = StartCoroutine(TickDownTimer());
        }
    }

    IEnumerator TickDownTimer()
    {
        
        while(roundTimer > 0)
        {
            yield return new WaitForSeconds(1f);
            roundTimer--;

            if(roundTimer == 10)
            {
                AudioManager.amInstance.Play("Last10Sec");
            }
        }

        TimerHit0();
    }

    public void PlayerDied(String tag)
    {
        if (currentGameState != MatchState.RoundInPlay) return;

        if(tag == "Player1")
        {
            p2Wins++;
            RoundEnds(GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerConfig>());
        }
        else
        {
            p1Wins++;
            RoundEnds(GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerConfig>());
        }
    }

     public void TimerHit0()
    {
        //Checks health and compares
        if (currentGameState != MatchState.RoundInPlay) return;

        PlayerHealth p1ShipHealth = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerHealth>();
        PlayerHealth p2ShipHealth = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerHealth>();

        if(p1ShipHealth.getCurrentHealth() > p2ShipHealth.getCurrentHealth())
        {
            p1Wins++;
            RoundEnds(p1ShipHealth.GetComponent<PlayerConfig>());
        }
        else
        { 
            p2Wins++;
            RoundEnds(p2ShipHealth.GetComponent<PlayerConfig>());
        }
    }

    private void RoundEnds(PlayerConfig roundWinnerPlayerConfig)
    {
        currentGameState = MatchState.RoundOver;
        if(timerCoroutine != null) StopCoroutine(timerCoroutine);

        Debug.Log("Round Ended.\nPlayer 1 Wins: " + p1Wins + "Player 2 Wins: " +p2Wins);

        if(hud != null) 
        {
            hud.SetWins(1,p1Wins);
            hud.SetWins(2,p2Wins);
        }

        if(p1Wins == 2 || p2Wins == 2)
        {
            MatchOver(roundWinnerPlayerConfig);
        }
        else
        {
            hud.SetBtwRoundsPanel(true, roundWinnerPlayerConfig, p1Wins, p2Wins);
            SetPlayerInput(false);
            StartCoroutine(LoadNextRound());
        }
    }
    private void MatchOver(PlayerConfig matchWinnerPlayerConfig)
    {
        currentGameState = MatchState.MatchOver;

        if(p1Wins == 2)
        {
            Debug.Log("Player 1 Won.");
        }
        else
        {
            Debug.Log("Player 2 Won.");
        }
        
        SetPlayerInput(false);
        hud.SetMatchOverPanel(true, matchWinnerPlayerConfig, p1Wins, p2Wins);
    }

    IEnumerator LoadNextRound()
    {

        if(currentGameState != MatchState.RoundOver) yield break;

        yield return new WaitForSeconds(5f);
        hud.SetBtwRoundsPanel(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StartCoroutine(PreRound());
    }

    private void SetPlayerInput(bool allowed)
    {
        PlayerInput p1Input = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerInput>();
        PlayerInput p2Input = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerInput>();

        if (allowed)
        {
            p1Input.ActivateInput();
            p2Input.ActivateInput();
        }
        else
        {
            p1Input.DeactivateInput();
            p2Input.DeactivateInput();
        }
    }

    public int GetCurrentRoundTime()
    {
        return roundTimer;
    
    }

    public ShipData GetShipData1()
    {
        return selectedShipData1;
    }

    public ShipData GetShipData2()
    {
        return selectedShipData2;
    }
}
