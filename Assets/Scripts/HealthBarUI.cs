using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image borderImage;   
    [SerializeField] private Image fillImage;

    private Slider slider;
    private PlayerHealth targetPlayer;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void Bind(PlayerConfig player)
    {
        if (targetPlayer != null) targetPlayer.OnHealthChanged -= UpdateBar;

        targetPlayer = player.GetComponent<PlayerHealth>();

        var shipData = player.GetShipData();
        borderImage.sprite = shipData.healthBarBorder;
        fillImage.color = shipData.fillColor;

        slider.maxValue = targetPlayer.MaxHealth;
        slider.value = targetPlayer.MaxHealth;

        targetPlayer.OnHealthChanged += UpdateBar;
    }

    void OnDisable()
    {
        if (targetPlayer != null) targetPlayer.OnHealthChanged -= UpdateBar;
    }

    void UpdateBar(float current, float max)
    {
        slider.value = current;
    }
}