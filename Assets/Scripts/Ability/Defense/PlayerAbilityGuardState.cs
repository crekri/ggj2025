using System;
using Bubble;
using Extensions;
using UnityEngine;

public class PlayerAbilityGuardState : PlayerAbilityStateBehaviour<PlayerAbilityGuardState.Config>
{
	public class Config : IStateConfig { }

	[SerializeField] private float ParryRecoveryTime = 0.5f;
	[SerializeField] private float ParryRadius = 1f;

	[SerializeField] private float ParryPower = 1.2f;
	[SerializeField] private float GuardHitPower = 1f;
	
	private float recoveryTimer = 0;
	[SerializeField] private AudioClip deflectSound;
	public override void OnEnter(Config config)
	{
		recoveryTimer = ParryRecoveryTime;
		AudioSource.PlayClipAtPoint(deflectSound, transform.position, 5f);
		
		var lastInputUnit = StateMachine.LastMoveInputUnit;
		var overlaps = Physics2D.OverlapCircleAll(transform.position, ParryRadius);
		var playerForward = lastInputUnit.normalized;
		var thisPlayerController = transform.GetComponentInParent<IPlayerController>();
		foreach (var collider in overlaps)
		{
			var player = collider.GetComponentInParent<IPlayerController>();
			if (player != null && player != thisPlayerController)
			{
				player.OnGuardHit(lastInputUnit, GuardHitPower);
			}
			else
			{
				var bubbleController = collider.GetComponentInParent<BubbleController>();
				if (bubbleController == null)
					continue;

				var canParry = Vector3.Dot(lastInputUnit, collider.transform.position - transform.position) > 0;
				if (canParry)
				{
					var delta = collider.transform.position - transform.position;
					delta.z = 0;
					delta.Normalize();
					//var parryDirection = lastInputUnit.SnapTo4Directions();
					bubbleController.Parry(delta, ParryPower);
				}
			}
		}
	}

	public override void OnUpdate()
	{
		recoveryTimer -= Time.deltaTime;
		if (recoveryTimer <= 0)
			StateMachine.TransitTo(new PlayerAbilityIdleState.Config(0f));
	}

	public override void OnExit(bool isCancel) { }

	private void OnDrawGizmos()
	{
		if (!Application.isPlaying)
			return;

		//Draws a 5 unit long red line in front of the object
		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, StateMachine.LastMoveInputUnit * ParryRadius);
	}
}