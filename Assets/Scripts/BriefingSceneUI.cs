using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class BriefingSceneUI : MonoBehaviour
{
    
    [SerializeField] private GameObject intelButton;
    public GameObject intelPanel;
    public GameObject controlsPanel;
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(intelButton);
    }
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

