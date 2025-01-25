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

		public AnimationClip idle;
		public AnimationClip walk;
		public AnimationClip trap;
		public AnimationClip jump_up;
		public AnimationClip jump_fall;

		public float AnimationSpeedMultiplier { get; private set; }

		[SerializeField] private float walkSpeedSpeedBase = 10f;

		public AnimationClip GetAnimationClip()
		{
			AnimationSpeedMultiplier = 1;

			switch (playerStateMachine.CurrentState)
			{
				case PlayerFreeState playerFreeState:
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
				default: throw new ArgumentOutOfRangeException();
			}
		}
	}
}