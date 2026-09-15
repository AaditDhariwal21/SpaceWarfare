using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShield : MonoBehaviour
{
    public PlayerStateMachine stateMachine;
    public GameObject shieldInstance;
    Animator shieldAnimator;

    public float raiseDuration = 0.25f;
    bool keyHeld;

    void Awake()
    {
        shieldAnimator = shieldInstance.GetComponent<Animator>();
        shieldInstance.SetActive(false);
    }

    void OnEnable() => stateMachine.OnStateChanged += HandleStateChanged;
    void OnDisable() => stateMachine.OnStateChanged -= HandleStateChanged;

    void HandleStateChanged(PlayerActionState prev, PlayerActionState next)
    {
         bool shouldShow = next == PlayerActionState.ShieldRaise || next == PlayerActionState.Shielding;

        if (shouldShow)
        {
            if (!shieldInstance.activeSelf) shieldInstance.SetActive(true);
            shieldAnimator.SetBool("ActivateShield", true);
        }
        else
        {
            shieldAnimator.SetBool("ActivateShield", false);
            StartCoroutine(DisableAfterDeactivate());
        }
    }

    public void OnShieldPressed()
    {
        keyHeld = true;
        BeginRaise();
    }

    public void OnShieldReleased()
    {
        keyHeld = false;
        ReleaseShield();
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

    IEnumerator DisableAfterDeactivate()
    {
        yield return new WaitForSeconds(raiseDuration);
        if (stateMachine.currentState != PlayerActionState.ShieldRaise &&
            stateMachine.currentState != PlayerActionState.Shielding)
        {
            shieldInstance.SetActive(false);
        }
    }
    
    public void InterruptRaise()
    {
        StopAllCoroutines(); 
        stateMachine.ChangeState(PlayerActionState.Neutral);
        if (keyHeld) BeginRaise(); // restart from 0 only if they're still holding it
    }

    public bool isShieldActive() => stateMachine.currentState == PlayerActionState.Shielding;
}