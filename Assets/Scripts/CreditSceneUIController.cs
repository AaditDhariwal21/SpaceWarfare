using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditSceneUIController : MonoBehaviour
{
    public void OnBackButtonClicked()
    {
        AudioManager.amInstance.Play("ButtonClick");
        SceneManager.LoadScene(0);
    }   
}
