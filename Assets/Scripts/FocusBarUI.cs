using UnityEngine;
using UnityEngine.UI;

public class FocusBarUI : MonoBehaviour
{
    public PlayerFocus targetPlayer;
    public Image[] segmentMarkers;
    private Slider slider;
    void Awake()
    {
        slider = GetComponent<Slider>();

        for(int i=0; i < segmentMarkers.Length; i++)
        {
            segmentMarkers[i].color = targetPlayer.GetComponent<PlayerWeapon>().shipData.fillColor;
        }
    }
    void OnEnable()
    {
        slider.maxValue = targetPlayer.MaxFocus; 
        slider.value = 0f;
    }

    void Update()
    {
        slider.value = targetPlayer.getCurrentFocus();
    }
}
