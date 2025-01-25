using System;
using System.Collections;
using System.Collections.Generic;
using Bubble;
using Chris;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovementController : MonoBehaviour, IPlayerController
{
	private PlayerParams playerParams;
	private Rigidbody2D rb;
	
	public float jumpHoldTime;
	public LayerMask groundLayer;
	public Transform groundCheck;
	
	private Vector2 velocity;
	private Vector2 moveInput;
	private float jumpInput;

	private bool _isJumpPressing;
	private int _numOfJump; //reset by the raycast on ground 
	private bool _isGrounded;
	private float _currentGravity;
	private float _velocityX;
	

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		playerParams = GetComponent<PlayerParams>();
		
	}

	public void SetMoveInput(Vector2 input01)
	{
		moveInput = input01;
	}

	
	public void SetJumpInput(bool isPressed)
	{
		if (isPressed)
		{
			if (_numOfJump <= 0)
			{
				return;
			}

			if (!_isJumpPressing)
			{
				_numOfJump-=1;
			}

			_isJumpPressing = true;
		}
		else
		{
			_isJumpPressing = false;
			jumpHoldTime = 0;
			jumpInput = 0;
		}

		if (isPressed)
		{
			
			rb.velocity = new Vector2(rb.velocity.x, playerParams.Stat.jumpVelocity);
		}
	}



	private void SetGround()
	{
		_isGrounded = true;
		_currentGravity = 0;
		_numOfJump = playerParams.Stat.numberOfJump;
	}

	private void SetAirborne()
	{
		_isGrounded = false;
		_currentGravity = playerParams.Stat.gravity;
		
	}


	
	private void FixedUpdate()
	{
		if (_isJumpPressing)
		{
			jumpHoldTime += Time.deltaTime;
			if (jumpHoldTime < playerParams.Stat.jumpHoldTime)
			{
				jumpInput += playerParams.Stat.jumpHoldStrengthRate * Time.fixedDeltaTime; //lerp required
			}
			else
			{
				jumpInput = 0;
			}
		}
		
		if (Physics2D.OverlapCircle(groundCheck.position, .1f, groundLayer))
		{
			if (!_isGrounded)
			{
				SetGround();
			}
		}
		else
		{
			if (_isGrounded)
			{
				SetAirborne();	
			}
		}

		if (!_isGrounded)
		{
			_currentGravity += playerParams.Stat.airBorneGravityIncreaseRate * Time.fixedDeltaTime; //lerp function required

		}
		                       

 
		var targetVelocity = new Vector2(moveInput.x * playerParams.Stat.moveVelocity, rb.velocity.y - _currentGravity + jumpInput * playerParams.Stat.jumpVelocity);
		rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, targetVelocity.x, ref _velocityX, playerParams.Stat.runDamp, Mathf.Infinity, Time.fixedDeltaTime), targetVelocity.y);

	}


	public bool IsFacingRight { get; private set; } = true;
	public void OnBubbleHit(BubbleHitInfo info)
	{
		
	}
}


public abstract class PlayerState : MonoBehaviour
{
	private PlayerStateMachine _stateMachine;
	protected void TransitTo(PlayerState newState) => _stateMachine.TransitTo(newState);

	public void Init(PlayerStateMachine stateMachine)
	{
		_stateMachine = stateMachine;
	}

	public abstract void OnEnter();

	public abstract void OnExit();
	
	public abstract void MyFixedUpdate();
	public abstract void SetMoveInput(Vector2 inputVector);
	public abstract void SetJumpInput(bool isPressed);

	public bool IsFacingRight;
}