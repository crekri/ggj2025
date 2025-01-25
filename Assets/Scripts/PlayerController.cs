using Bubble;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerController
{
    public PlayerStateMachine playerStateMachine;
    public void SetMoveInput(Vector2 readValue)
    {
        playerStateMachine.CurrentState.SetMoveInput(readValue);
    }

    public void SetJumpInput(bool readValueAsButton)
    {
        playerStateMachine.CurrentState.SetJumpInput(readValueAsButton);
    }

    public bool IsFacingRight => playerStateMachine.CurrentState.IsFacingRight;
    public void OnBubbleHit(BubbleHitInfo info)
    {
        playerStateMachine.OnBubbleHit(info);
    }

    public void OnGuardHit(Vector2 lastInputUnit, float guardHitPower)
    {
        Debug.Log("Nick you do this");
    }
}