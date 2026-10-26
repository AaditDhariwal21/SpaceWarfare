using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
      public void PlayButtonClick()
    {
        AudioManager.amInstance.Play("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BriefingButtonClick()
    {
        AudioManager.amInstance.Play("ButtonClick");
        SceneManager.LoadScene(3);
    }

    public void CreditButtonClick()
    {
        AudioManager.amInstance.Play("ButtonClick");
        SceneManager.LoadScene(4);
    }

}
