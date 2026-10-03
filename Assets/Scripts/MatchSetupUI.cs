using UnityEngine;
using UnityEngine.UI;

public class MatchSetupUI : MonoBehaviour
{   
    [SerializeField] ShipSelectPanel[] panels;
    public Button startButton;
    void Update()
    {
        foreach(ShipSelectPanel p in panels)
        {
            if(!p.GetCurrentState()) return;
        }

        startButton.interactable = true;
    }
    public void OnStartButtonPressed()
    {
        GameManager.gmInstance.TryMatchStart(); 
   }
}
