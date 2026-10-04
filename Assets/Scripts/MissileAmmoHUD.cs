using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MissileAmmoHUD : MonoBehaviour
{
    public Image missileImage;
    public TextMeshProUGUI ammoCount;

    private PlayerWeapon targetPlayerWeapon;
    public void Bind(PlayerConfig playerConfig)
    {
        missileImage.sprite = playerConfig.GetShipData().missileSprite;

        targetPlayerWeapon = playerConfig.GetComponent<PlayerWeapon>();
    }

    void Update()
    {
        ammoCount.text = "x" + targetPlayerWeapon.GetCurrentMissileAmmoCount();

        if(targetPlayerWeapon.GetCurrentMissileAmmoCount() == 0)
        {
            missileImage.GetComponent<Animator>().SetBool("Reloading", true);
        }
        else
        {
            missileImage.GetComponent<Animator>().SetBool("Reloading", false);
        }
    }
}
