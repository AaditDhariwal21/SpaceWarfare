using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gmInstance;

    private ShipData selectedShipData1;
    private ShipData selectedShipData2;
    private int roundNumber = 0;
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
        }    
    }

    public void PlayButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void TryMatchStart()
    {
        ShipSelectPanel[] panels = FindObjectsByType<ShipSelectPanel>(FindObjectsSortMode.None);
        Array.Sort(panels, (a, b) => a.playerIndex.CompareTo(b.playerIndex));

        foreach (ShipSelectPanel panel in panels)
        {
            if(!panel.GetCurrentState())
            {
                return;
            }
        }
        Debug.Log("Both Players are ready.");

        selectedShipData1 = panels[0].GetCurrentShipData();
        selectedShipData2 = panels[1].GetCurrentShipData();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
