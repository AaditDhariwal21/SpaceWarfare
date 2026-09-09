using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShield : MonoBehaviour
{
    public PlayerStateMachine stateMachine;
    public float raiseDuration = 0.25f;
    bool keyHeld;

    public void OnShield(InputValue input)
    {
        keyHeld = input.isPressed;
        if (keyHeld) BeginRaise();
        else ReleaseShield();
    }

    void BeginRaise()
    {
        if (stateMachine.currentState == PlayerActionState.UsingSpecial) return;
        stateMachine.ChangeState(PlayerActionState.ShieldRaise);
        StartCoroutine(FinishRaise());
    }

    IEnumerator FinishRaise()
    {
        yield return new WaitForSeconds(raiseDuration);
        if (stateMachine.currentState == PlayerActionState.ShieldRaise)
            stateMachine.ChangeState(PlayerActionState.Shielding);
    }

    void ReleaseShield()
    {
        if (stateMachine.currentState == PlayerActionState.ShieldRaise ||
            stateMachine.currentState == PlayerActionState.Shielding)
            stateMachine.ChangeState(PlayerActionState.Neutral);
    }

    public bool isShieldActive() => stateMachine.currentState == PlayerActionState.Shielding;

    // Called by Projectile when a hit lands mid-raise
    public void InterruptRaise()
    {
        StopAllCoroutines(); // kill the in-flight FinishRaise timer — see below for why this matters
        stateMachine.ChangeState(PlayerActionState.Neutral);
        if (keyHeld) BeginRaise(); // restart from 0 only if they're still holding it
    }
}