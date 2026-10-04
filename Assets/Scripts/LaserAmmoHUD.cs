using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class LaserAmmoHUD : MonoBehaviour
{
    public Image laserImage;
    public TextMeshProUGUI ammoCount;
    private PlayerWeapon targetPlayerWeapon;
    public void Bind(PlayerConfig playerConfig)
    {
        laserImage.sprite = playerConfig.GetShipData().laserSprite;

        targetPlayerWeapon = playerConfig.GetComponent<PlayerWeapon>();
    }

    void Update()
    {
        ammoCount.text = "x" + targetPlayerWeapon.GetCurrentLaserAmmoCount();

        if(targetPlayerWeapon.GetCurrentLaserAmmoCount() == 0)
        {
            laserImage.GetComponent<Animator>().SetBool("Reloading", true);
        }
        else
        {
            laserImage.GetComponent<Animator>().SetBool("Reloading", false);
        }
    }
}
