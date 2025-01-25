using Bubble;
using UnityEngine;

public interface IPlayerController
{
	bool IsFacingRight { get; }
	void SetMoveInput(Vector2 readValue);
	void SetJumpInput(bool readValueAsButton);
	void OnBubbleHit(BubbleHitInfo info);
}

public static class IPlayerControllerExtensions
{
	public static Vector2 GetOrientation(this IPlayerController playerController)
	{
		return playerController.IsFacingRight ? Vector2.right : Vector2.left;
	}
}