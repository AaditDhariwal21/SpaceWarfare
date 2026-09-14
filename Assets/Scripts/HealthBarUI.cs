using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{

    public PlayerHealth targetPlayer;
    public Image borderImage;
    public Image fillImage;

    private Slider slider;

     void Awake()
    {
        slider = GetComponent<Slider>();

        var shipData = targetPlayer.GetComponent<PlayerWeapon>().shipData;
        fillImage.color = shipData.fillColor;
        borderImage.sprite = shipData.healthBarBorder;
    }

    void OnEnable()
    {
        targetPlayer.OnHealthChanged += UpdateBar; //Subscribing to the action
        slider.maxValue = targetPlayer.MaxHealth; 
        slider.value = targetPlayer.MaxHealth;
    }

    void OnDisable()
    {
        targetPlayer.OnHealthChanged -= UpdateBar; // unsubscribing from the action
    }

    void UpdateBar(float current, float max)
    {
        slider.value = current;
    }
   
}
