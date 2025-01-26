using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Animation
{
	public class PlayerAnimationController : MonoBehaviour
	{
		[SerializeField] private Animator animator;
		[SerializeField] private AnimationDecisionTree decisionTree;

		private AnimationClip currentClip = null;

		private void Update()
		{
			var newClip = decisionTree.GetAnimationClip();
			if (newClip == currentClip)
				return;

			Debug.Log($"PlayerAnimationController.Update: newClip={newClip.name}");
			currentClip = newClip;
			animator.Play(newClip.name);
			animator.speed = decisionTree.AnimationSpeedMultiplier;
		}
	}

	[Serializable]
	public class AnimationDecisionTree
	{
		[SerializeField] private PlayerStateMachine playerStateMachine;
		[SerializeField] private PlayerAbilityStateMachine abilityStateMachine;

		public AnimationClip idle;
		public AnimationClip walk;
		public AnimationClip trap;
		public AnimationClip jump_up;
		public AnimationClip jump_fall;
		public AnimationClip attack_charge;
		public AnimationClip attack_Charge_complete;
		public AnimationClip attack_release;

		public float AnimationSpeedMultiplier { get; private set; }

		[SerializeField] private float walkSpeedSpeedBase = 10f;

		public AnimationClip GetAnimationClip()
		{
			AnimationSpeedMultiplier = 1;

			switch (playerStateMachine.CurrentState)
			{
				case PlayerFreeState playerFreeState:
					var abilityClip = GetAnimationClipFromAbilityOrNull();
					if (abilityClip != null)
						return abilityClip;

					if (!playerFreeState.IsGrounded)
						return playerFreeState.VelocityY > 0 ? jump_up : jump_fall;

					var absVelocityX = Mathf.Abs(playerFreeState.VelocityX);
					if (absVelocityX > 2f)
					{
						AnimationSpeedMultiplier = absVelocityX / walkSpeedSpeedBase;
						return walk;
					}

					return idle;
				case PlayerTrapState playerTrapState:
					return trap;
				case PlayerInvisibleState invisibleState:
					return idle;
				default: throw new ArgumentOutOfRangeException();
			}
		}

		private AnimationClip GetAnimationClipFromAbilityOrNull()
		{
			switch (abilityStateMachine.CurrentState)
			{
				case PlayerAbilityIdleState playerAbilityIdleState:
					if (playerAbilityIdleState.FromStateNullable is PlayerAbilityBubbleChargeState)
						return attack_release;
					break;
				case PlayerAbilityBubbleChargeState playerAbilityBubbleChargeState:
					if (playerAbilityBubbleChargeState.BigBubbleChargedAndCanFire)
						return attack_Charge_complete;
					return attack_charge;
				case PlayerAbilityGuardState playerAbilityGuardState: break;
				default: throw new ArgumentOutOfRangeException();
			}

			return null;
		}
	}
}