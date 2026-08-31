using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    public Transform firePoint;
    public ShipData shipData;


    public void OnShootLasers(InputValue input)
    {
        if(!input.isPressed) return;
        Fire(shipData.laserPrefab);
    }

    public void OnShootMissiles(InputValue input)
    {
        if (!input.isPressed) return;
        Fire(shipData.missilePrefab);
    }

     void Fire(GameObject prefab)
    {
        if (prefab == null || firePoint == null) return;

        var proj = Instantiate(prefab, firePoint.position, firePoint.rotation);
        proj.GetComponent<Projectile>().Launch(firePoint.up);
    }
}
