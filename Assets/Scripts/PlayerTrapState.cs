using UnityEngine;

public class PlayerTrapState : PlayerState
{
	//Movement control 
	[SerializeField] private GameObject bubbleTrapVisual;
	[SerializeField] private PlayerParams playerParams;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private PlayerFreeState playerFreeState;
	[SerializeField] private Transform flipTransform;
	private Vector2 velocity;
	private Vector2 moveInput;
	private float jumpInput;

	private bool _isJumpPressing;
	private int _numOfJump; //reset by the raycast on ground 
	private bool _isGrounded;
	private float _currentGravity;
	private float _velocityX;

	private float _currentLifeTime;

	public override void OnEnter()
	{
		rb.velocity *= .25f;
		moveInput = Vector2.zero;
		_currentLifeTime = playerParams.Stat.trapTimerMult;
		bubbleTrapVisual.SetActive(true);
	}

	public override void OnExit()
	{
		bubbleTrapVisual.SetActive(false);
		rb.velocity = Vector2.zero;
	}

	public override void MyFixedUpdate()
	{
		if (_currentLifeTime > 0)
		{
			_currentLifeTime -= Time.deltaTime;


			var targetVelocity = new Vector2(rb.velocity.x, rb.velocity.y + 2f * Time.fixedDeltaTime);
			
			rb.velocity = targetVelocity;
			
			rb.velocity = targetVelocity + new Vector2(moveInput.x * 5f * Time.fixedDeltaTime,
				moveInput.y * 5f * Time.fixedDeltaTime);
			 
			if (Mathf.Abs(moveInput.x) > .5f)
			{
				IsFacingRight = moveInput.x > 0;
				flipTransform.localScale = new Vector3(IsFacingRight ? 1 : -1, 1, 1);
			}
		}
		else
		{
			TransitTo(playerFreeState);
		}
	}

	public override void SetMoveInput(Vector2 inputVector)
	{
		moveInput = inputVector;
	}

	public override void SetJumpInput(bool isPressed)
	{
		
	}

	public override void SetAnyKeyDown()
	{
		_currentLifeTime -= playerParams.Stat.reduceTrapPerClick;
	}
}