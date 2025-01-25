using Bubble;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerAbilityBubbleChargeState : PlayerAbilityStateBehaviour
{
	[SerializeField] private PlayerAbilityIdleState idleState;

	[SerializeField] private MonoBehaviour playerControllerObject;
	private IPlayerController playerController => playerControllerObject as IPlayerController;
	[SerializeField] private PlayerAmmoController ammoController;
	[SerializeField] private float smallAmmoCost = 1f;
	[SerializeField] private float bigAmmoCost = 3f;

	[SerializeField] private BubbleController smallBubblePrefab;
	[SerializeField] private BubbleController bigBubblePrefab;
	[SerializeField] private float BigBubbleChargeDuration = .5f;

	[SerializeField] private Transform bubbleShootPoint;

	private float bubbleChargeTimer;

	private bool BigBubbleChargedAndCanFire => bubbleChargeTimer <= 0 && ammoController.HasEnoughAmmo(bigAmmoCost);

	public override void OnEnter()
	{
		bubbleChargeTimer = BigBubbleChargeDuration;
	}

	public override void OnUpdate()
	{
		bubbleChargeTimer -= Time.deltaTime;

		/*
		// If want to support auto fire when reach time uncomment this
		if (BigBubbleChargedAndCanFire)
		{
			TransitionTo(idleState);
			return;
		}
		*/
	}

	public override void OnExit(bool isCancel)
	{
		if (isCancel)
			return;

		if (BigBubbleChargedAndCanFire)
			FireBubble(bigBubblePrefab, bigAmmoCost);
		else
			FireBubble(smallBubblePrefab, smallAmmoCost);
	}

	private void FireBubble(BubbleController bubblePrefab, float ammoCost)
	{
		var success = ammoController.TryConsumeAmmo(ammoCost);
		Assert.IsTrue(success);

		var bubble = Instantiate(bubblePrefab, bubbleShootPoint.position, Quaternion.identity);
		bubble.Setup(playerController.GetOrientation());
	}

	public override void OnBubbleButtonRelease()
	{
		TransitionTo(idleState);
	}
}