using Bubble;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerController
{
	public int Id { get; private set; }
	public PlayerStateMachine playerStateMachine;
	public PlayerAbilityStateMachine playerAbilityStateMachine;

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
		playerStateMachine.OnGuardHit(lastInputUnit, guardHitPower);
	}

	public void SetAnyKeyDown()
	{
		playerStateMachine.CurrentState.SetAnyKeyDown();
	}

	public void Init(Vector3 spawnPosition, int id)
	{
		Id = id;
		playerStateMachine.SetPosition(spawnPosition);
	}
}