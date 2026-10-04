using UnityEngine;
using UnityEngine.SceneManagement;

public class BriefingSceneUI : MonoBehaviour
{
    
    public GameObject intelPanel;
    public GameObject controlsPanel;

    public void OnDismissClick()
    {
        SceneManager.LoadScene(0);
    }

    public void OnIntelClick()
    {
        intelPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void OnControlsClick()
    {
        intelPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }
}
