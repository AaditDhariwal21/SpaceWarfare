using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    public Transform firePoint;
    public ShipData shipData;

    public GameObject laserPrefab;
    public GameObject missilePrefab;

    [Header("Ammo Config")]
    public int laserMaxAmmo = 15;
    public int missileMaxAmmo = 5;
    public float laserReloadTime = 3f;
    public float missileReloadTime = 5f;

    public PlayerStateMachine stateMachine;

    int laserAmmo;
    int missileAmmo;
    bool laserReloading;
    bool missileReloading;

    void Awake()
    {
        laserAmmo = laserMaxAmmo;
        missileAmmo = missileMaxAmmo;

        laserReloading = false;
        missileReloading = false;

        GetComponent<SpriteRenderer>().sprite = shipData.playerSprite;
    }

    public void OnShootLasers(InputValue input)
    {
        if(!input.isPressed) return;
        if (stateMachine.currentState != PlayerActionState.Neutral) return;
        if (laserReloading || laserAmmo <= 0) return;

    
        Fire(laserPrefab, shipData.laserSprite);
        laserAmmo--;
        if(laserAmmo <= 0 ) StartCoroutine(LaserReload());
    }

    public void OnShootMissiles(InputValue input)
    {
        if(!input.isPressed) return;
        if (stateMachine.currentState != PlayerActionState.Neutral) return;
        if (missileReloading || missileAmmo <= 0) return;

        Fire(missilePrefab, shipData.missileSprite);
        missileAmmo--;
        if(missileAmmo <= 0) StartCoroutine(MissileReload());
    }

    void Fire(GameObject prefab, Sprite sprite)
    {
        if (prefab == null || firePoint == null) return;

        var proj = Instantiate(prefab, firePoint.position, firePoint.rotation);
        proj.GetComponent<SpriteRenderer>().sprite=sprite;
        proj.GetComponent<Projectile>().Launch(firePoint.up);
    }

    IEnumerator LaserReload()
    {
        laserReloading = true;
        yield return new WaitForSeconds(laserReloadTime);
        laserAmmo = laserMaxAmmo;
        laserReloading = false;
    }

    IEnumerator MissileReload()
    {
        missileReloading = true;
        yield return new WaitForSeconds(missileReloadTime);
        missileAmmo = missileMaxAmmo;
        missileReloading = false;
    }

}
