using UnityEngine;
using UnityEngine.SceneManagement;

public class BriefingSceneUI : MonoBehaviour
{
    
    public GameObject intelPanel;
    public GameObject controlsPanel;

    public void OnDismissClick()
    {
        AudioManager.amInstance.Play("ButtonClick");
        SceneManager.LoadScene(0);
    }

    public void OnIntelClick()
    {
        AudioManager.amInstance.Play("ButtonClick");
        intelPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void OnControlsClick()
    {
        AudioManager.amInstance.Play("ButtonClick");
        intelPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }
}
