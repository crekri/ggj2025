namespace Chris
{
	using UnityEngine;

	public class ChrisTesterPlayerMovementController : MonoBehaviour, IPlayerController
	{
		private Rigidbody2D rb;

		public float RunSpeed = 10f;
		public float RunDamp = 0.15f;
		public float JumpVelocity = 10f;

		private Vector2 velocity;
		private Vector2 moveInput;

		[SerializeField] private Transform flipTransform;

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
			if (isPressed)
				rb.velocity = new Vector2(rb.velocity.x, JumpVelocity);
		}

		private void FixedUpdate()
		{
			var targetVelocity = new Vector2(moveInput.x * RunSpeed, rb.velocity.y);
			rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, RunDamp, Mathf.Infinity, Time.fixedDeltaTime);
			if (Mathf.Abs(rb.velocity.x) > .5f)
			{
				IsFacingRight = rb.velocity.x > 0;
				flipTransform.localScale = new Vector3(IsFacingRight ? 1 : -1, 1, 1);
			}
		}

		public bool IsFacingRight { get; private set; } = true;
	}
}