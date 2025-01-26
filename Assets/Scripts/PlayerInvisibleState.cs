using UnityEngine;

public class PlayerInvisibleState : PlayerState
{
	[SerializeField] private Rigidbody2D rb;
	
	public override void OnEnter()
	{
		rb.velocity = Vector2.zero;
	}

	public override void OnExit()
	{
		rb.velocity = Vector2.zero;
	}

	public override void MyFixedUpdate()
	{
		rb.velocity = Vector2.zero;
	}

	public override void SetMoveInput(Vector2 inputVector)
	{
	}

	public override void SetJumpInput(bool isPressed)
	{
	}

	public override void SetAnyKeyDown()
	{
	}
}