using Bubble;
using Match;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerController
{
	public int Id { get; private set; }
	public MatchPlayerConfig Config { get; private set; }
	public PlayerStateMachine playerStateMachine;
	public PlayerAbilityStateMachine playerAbilityStateMachine;

	[SerializeField] private SpriteRenderer playerColorSpriteRenderer;
	
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

	public void OnPowerUpHit(IPowerUpHandler powerUpHandler)
	{
		return;
	}

	public void SetAnyKeyDown()
	{
		playerStateMachine.CurrentState.SetAnyKeyDown();
	}

	public void Init(Vector3 spawnPosition, int id, MatchPlayerConfig matchPlayerConfig)
	{
		Id = id;
		Config = matchPlayerConfig;
		playerStateMachine.SetPosition(spawnPosition);
		var color = matchPlayerConfig.Color;
		color.a = .5f;
		playerColorSpriteRenderer.color = color;
	}
	
	public void OnDestroy()
	{
		MatchStateGame.Instance.OnPlayerKilled(this);
	}
}