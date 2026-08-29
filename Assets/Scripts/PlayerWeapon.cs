using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    
    public void OnShootLasers(InputValue input)
    {
        Debug.Log(input.isPressed);
    }

    public void OnShootMissiles(InputValue input)
    {
        Debug.Log(input.isPressed);
    }
}
