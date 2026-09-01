using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShield : MonoBehaviour
{

    private bool isActive;

    void Awake()
    {
        isActive = false;
    }

    void OnShield(InputValue input)
    {
        isActive = input.isPressed;
    }

    public bool isShieldActive()
    {
        return isActive;    
    }
}
