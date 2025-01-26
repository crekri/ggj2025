using Bubble;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class PlayerAbilityBubbleChargeState : PlayerAbilityStateBehaviour<PlayerAbilityBubbleChargeState.Config>
{
	public class Config : IStateConfig { }

	[SerializeField] private float bubbleStartingPosition = 1;
	[SerializeField] private MonoBehaviour playerControllerObject;
	[SerializeField] private PlayerAmmoController ammoController;
	[SerializeField] private float smallAmmoCost = 1f;
	[SerializeField] private float bigAmmoCost = 3f;

	[SerializeField] private BubbleController smallBubblePrefab;
	[SerializeField] private BubbleController bigBubblePrefab;
	[SerializeField] private float BigBubbleChargeDuration = .5f;

	private float bubbleChargeTimer;

	public bool BigBubbleChargedAndCanFire => bubbleChargeTimer <= 0 && ammoController.HasEnoughAmmo(bigAmmoCost);

	public override void OnEnter(Config config)
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
		if (!success)
			return;

		var shootDirection = StateMachine.LastMoveInputUnit;
		shootDirection.x = Mathf.Sign(shootDirection.x);
		shootDirection.y = 0;

		var bubbleShootPoint = transform.position + (Vector3) shootDirection * bubbleStartingPosition;
		var bubble = Instantiate(bubblePrefab, bubbleShootPoint, Quaternion.identity);
		bubble.ApplyDirection(shootDirection);
	}

	public override void OnBubbleButtonRelease()
	{
		StateMachine.TransitTo(new PlayerAbilityIdleState.Config());
	}
}