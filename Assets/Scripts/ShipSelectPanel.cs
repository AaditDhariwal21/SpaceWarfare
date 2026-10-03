using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;

public class ShipSelectPanel : MonoBehaviour
{
    public int playerIndex;
    public Image shipImage;
    public TextMeshProUGUI shipName;
    public Image specialImage;
    public TextMeshProUGUI specialName;

    [SerializeField] ShipData[] allShipData;
    private int currentIndex;
    private bool isReady = false;

    void Start()
    {
        ShowCurrentShip();
    }

    void ShowCurrentShip()
    {
        shipImage.sprite = allShipData[currentIndex].playerSprite;
        shipName.text = allShipData[currentIndex].shipName;
        shipName.color = allShipData[currentIndex].fillColor;
        specialName.text = "Special :\n" + allShipData[currentIndex].specialName;
    }
    public void OnNextPressed()
    {
        currentIndex++;
        if(currentIndex >= 3) currentIndex = 0;
        ShowCurrentShip();
    }
    public void OnPrevPressed()
    {
        currentIndex--;
        if(currentIndex <= -1) currentIndex = 2;
        ShowCurrentShip();
    }

    public void OnReady()
    {
        isReady = true;
        Button[] allButtons = GetComponentsInChildren<Button>();

        foreach(Button b in allButtons)
        {
            if (b.CompareTag("ChevronButton"))
            {
                b.GetComponentInChildren<TextMeshProUGUI>().color = Color.gray2;
            }
            b.interactable = false;
        }
    }

    public bool GetCurrentState()
    {
        return isReady;
    }

    public ShipData GetCurrentShipData()
    {
        return allShipData[currentIndex];
    }
}
