using Bubble;
using UnityEngine;

public interface IPlayerController : IPlayerMovementController
{
	bool IsFacingRight { get; }
	void OnBubbleHit(BubbleHitInfo info);
	void OnGuardHit(Vector2 lastInputUnit, float guardHitPower);
	
}

public interface IPlayerMovementController
{
	void SetMoveInput(Vector2 readValue);
	void SetJumpInput(bool readValueAsButton);
	void SetAnyKeyDown();
}

public static class IPlayerControllerExtensions
{
	public static Vector2 GetOrientation(this IPlayerController playerController)
	{
		return playerController.IsFacingRight ? Vector2.right : Vector2.left;
	}
}