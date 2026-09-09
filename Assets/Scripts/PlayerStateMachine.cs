using System;
using UnityEngine;

public enum PlayerActionState { Neutral, ShieldRaise, Shielding, UsingSpecial}

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerActionState currentState;
    public event Action<PlayerActionState, PlayerActionState> OnStateChanged;
    public PlayerActionState getPlayerActionState()
    {
        return currentState;
    }

    public void ChangeState(PlayerActionState next) {
        if (next == currentState) return;
        var prev = currentState;
        currentState = next;
        OnStateChanged?.Invoke(prev, next); // animation/anim events, health bar, VFX all subscribe here
    }
}
