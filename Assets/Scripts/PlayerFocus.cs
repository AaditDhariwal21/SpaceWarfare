using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFocus : MonoBehaviour
{

    private float maxFocus = 90f;
    public float MaxFocus => maxFocus;
    private float currentFocus;
    private int segmentCount = 3;
    private float focusGenPerSecond = 1f;

    void Awake()
    {
        currentFocus = 0f;
    }
    void Update()
    {
        currentFocus = Math.Min(maxFocus, currentFocus + (focusGenPerSecond * Time.deltaTime));
    }
    public float getCurrentFocus()
    {
        return currentFocus;
    }

    public void AddFocus(float blockedDamage)
    {
        currentFocus = Math.Min(currentFocus + (float)(0.5 * blockedDamage / 5), maxFocus); 
    }

    public void OnSpecial(InputValue input)
    {
        if(!input.isPressed) return;
        if(currentFocus < 30) return;

        if(currentFocus > 30)
        {
            currentFocus-=currentFocus;
        }
    }
}
