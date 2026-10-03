using UnityEngine;
using UnityEngine.UI;

public class FocusBarUI : MonoBehaviour
{
    [SerializeField] private Image[] segmentMarkers;
    private Slider slider;
    private PlayerFocus targetPlayer;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void Bind(PlayerConfig player)
    {
        targetPlayer = player.GetComponent<PlayerFocus>();

        Color fill = player.GetShipData().fillColor;
        for (int i = 0; i < segmentMarkers.Length; i++)
        {
            segmentMarkers[i].color = fill;
        }

        slider.maxValue = targetPlayer.MaxFocus;
        slider.value = 0f;
    }

    void Update()
    {
        if (targetPlayer == null) return;
        slider.value = targetPlayer.getCurrentFocus();
    }
}