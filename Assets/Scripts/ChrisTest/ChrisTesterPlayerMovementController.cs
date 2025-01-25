using System;
using Bubble;

namespace Chris
{
	using UnityEngine;

	public class ChrisTesterPlayerMovementController : MonoBehaviour, IPlayerController
	{
		public enum EState
		{
			Free,
			Bubble,
		}

		private Rigidbody2D rb;

		public float RunSpeed = 10f;
		public float RunDamp = 0.15f;
		public float JumpVelocity = 10f;

		private Vector2 velocity;
		private Vector2 moveInput;

		[SerializeField] private Transform flipTransform;

		public float SoapAmount { get; private set; }
		[SerializeField] private TextMesh soapAmountText;

		public EState State { get; private set; } = EState.Free;
		public float BubbleCountdown;

		private void Awake()
		{
			rb = GetComponent<Rigidbody2D>();
		}

		public void SetMoveInput(Vector2 input01)
		{
			moveInput = input01;
		}

		public void SetJumpInput(bool isPressed)
		{
			switch (State)
			{
				case EState.Free:
					if (isPressed)
						rb.velocity = new Vector2(rb.velocity.x, JumpVelocity);
					break;
				case EState.Bubble:
					if (isPressed)
					{
						rb.velocity = new Vector2(rb.velocity.x, JumpVelocity * .1f);
						BubbleCountdown -= .1f;
					}

					break;
				default: throw new ArgumentOutOfRangeException();
			}
		}

		public void OnBubbleHit(BubbleHitInfo info)
		{
			SoapAmount += info.SoapAmount;
			soapAmountText.text = SoapAmount.ToString("P0");

			if (info.IsTrap)
			{
				BubbleCountdown = .5f + SoapAmount;
				State = EState.Bubble;
			}
		}

		private void FixedUpdate()
		{
			switch (State)
			{
				case EState.Free:
				{
					soapAmountText.text = SoapAmount.ToString("P0");
					var targetVelocity = new Vector2(moveInput.x * RunSpeed, rb.velocity.y);
					rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, RunDamp, Mathf.Infinity, Time.fixedDeltaTime);
					if (Mathf.Abs(rb.velocity.x) > .5f)
					{
						IsFacingRight = rb.velocity.x > 0;
						flipTransform.localScale = new Vector3(IsFacingRight ? 1 : -1, 1, 1);
					}
				}
					break;
				case EState.Bubble:
				{
					soapAmountText.text = "TRAPPED!" + BubbleCountdown.ToString("F2");

					BubbleCountdown -= Time.fixedDeltaTime;
					if (BubbleCountdown <= 0)
						State = EState.Free;
				}
					break;
				default: throw new ArgumentOutOfRangeException();
			}
		}

		public bool IsFacingRight { get; private set; } = true;
	}
}