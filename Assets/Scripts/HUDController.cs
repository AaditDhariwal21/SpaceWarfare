using System;
using System.Collections;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("Player 1 bars")]
    [SerializeField] private HUDFader p1HUD;
    [SerializeField] private HealthBarUI p1HealthBar;
    [SerializeField] private FocusBarUI p1FocusBar;
    [SerializeField] private LaserAmmoHUD p1LaserAmmoHUD;
    [SerializeField] private MissileAmmoHUD p1MissileAmmoHUD;

    [Header("Player 2 bars")]
    [SerializeField] private HUDFader p2HUD;
    [SerializeField] private HealthBarUI p2HealthBar;
    [SerializeField] private FocusBarUI p2FocusBar;
    [SerializeField] private LaserAmmoHUD p2LaserAmmoHUD;
    [SerializeField] private MissileAmmoHUD p2MissileAmmoHUD;

    [SerializeField] private Image roundwinP1;
    [SerializeField] private Image roundwinP2;
    
    [SerializeField] private Sprite[] roundWinCounterImages;
    [SerializeField] private TextMeshProUGUI preroundTimer;
    [SerializeField] private GameObject pausePanel;

    [Header("Between Rounds Panel")]
    [SerializeField] private GameObject btwRoundsPanel;
    [SerializeField] private TextMeshProUGUI whichPlayerWonText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Match Over Panel")]
    [SerializeField] private GameObject matchOverPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI matchWinnerText;



    void Start()
    {
        if (GameManager.gmInstance != null) GameManager.gmInstance.RegisterHUD(this);

        
    }
    public void BindPlayers(PlayerConfig player1, PlayerConfig player2)
    {

        p1HUD.Bind(player1.GetComponent<SpriteRenderer>());
        p1HealthBar.Bind(player1);
        p1FocusBar.Bind(player1);
        p1LaserAmmoHUD.Bind(player1);
        p1MissileAmmoHUD.Bind(player1);

        p2HUD.Bind(player2.GetComponent<SpriteRenderer>());
        p2HealthBar.Bind(player2);
        p2FocusBar.Bind(player2);
        p2LaserAmmoHUD.Bind(player2);
        p2MissileAmmoHUD.Bind(player2);
    }

    public void SetWins(int playerNumber, int playerWins)
    {
        if(playerNumber == 1)
        {
            roundwinP1.sprite = roundWinCounterImages[playerWins];
        }
        else
        {
            roundwinP2.sprite = roundWinCounterImages[playerWins];
        }
    }
    
    public void SetPausePanel(bool enabled)
    {
        pausePanel.SetActive(enabled);
    }

    public IEnumerator RunPreroundTimer(int sec)
    {
        preroundTimer.gameObject.SetActive(true);

        preroundTimer.text = "" + sec;
        int timer = sec;
    
        while (timer >= 0)
        {
            yield return new WaitForSeconds(1f);
            timer--;
            preroundTimer.text = ""+timer;
        }

        preroundTimer.gameObject.SetActive(false);
    }

    public void SetBtwRoundsPanel(bool enabled, PlayerConfig roundWinnerPlayerConfig, int p1Wins, int p2Wins)
    {
        btwRoundsPanel.SetActive(enabled);

        whichPlayerWonText.text = roundWinnerPlayerConfig.GetShipData().shipName + " (player " + roundWinnerPlayerConfig.GetPlayerNumber() + ") won this round";
        scoreText.text = "SCORE : " + p1Wins + " - " + p2Wins;
    }

    public void SetBtwRoundsPanel(bool enabled)
    {
        btwRoundsPanel.SetActive(enabled);
    }

    public void SetMatchOverPanel(bool enabled, PlayerConfig matchWinnerPlayerConfig, int p1Wins, int p2Wins)
    {
        matchOverPanel.SetActive(enabled);

        finalScoreText.text = "SCORE : " + p1Wins + " - " + p2Wins;
        matchWinnerText.text = matchWinnerPlayerConfig.GetShipData().shipName + " (player " + matchWinnerPlayerConfig.GetPlayerNumber() + ") WINS";
        
    }

    public void SetMatchOverPanel(bool enabled)
    {
        matchOverPanel.SetActive(enabled);
    }

    public void BackToMenuButtonClicked()
    {
        AudioManager.amInstance.Play("ButtonClick");
        AudioManager.amInstance.Stop("FightTheme");
        AudioManager.amInstance.PlayMusic("GameTheme");
        SceneManager.LoadScene(0);
    }
}
