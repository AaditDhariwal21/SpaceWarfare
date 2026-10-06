using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFocus : MonoBehaviour
{

    private float maxFocus = 90f;
    public float MaxFocus => maxFocus;
    private float currentFocus;
    private float focusGenPerSecond = 1f;
    private const float specialCost = 30f;

    private PlayerConfig playerConfig;
    private PlayerStateMachine stateMachine;

    void Awake()
    {
        playerConfig = GetComponent<PlayerConfig>();
        stateMachine = GetComponent<PlayerStateMachine>();
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
        if (stateMachine.currentState == PlayerActionState.UsingSpecial) return;
        if(currentFocus < specialCost) return;

        currentFocus -= specialCost;

        SpecialController sc = Instantiate(playerConfig.GetShipData().specialControllerPrefab,transform.position,transform.rotation);
        stateMachine.ChangeState(PlayerActionState.UsingSpecial);
        sc.OnFinished += HandleSpecialFinished;
        sc.Activate(transform);   
    }

    void HandleSpecialFinished()
    {
        stateMachine.ChangeState(PlayerActionState.Neutral);
    }
}
