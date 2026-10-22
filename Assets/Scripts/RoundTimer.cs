using TMPro;
using UnityEngine;

public class RoundTimer : MonoBehaviour
{
    private TextMeshProUGUI timerText;

    void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        timerText.text = "" + GameManager.gmInstance.GetCurrentRoundTime();
    }
}
