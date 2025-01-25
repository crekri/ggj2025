using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovementController : MonoBehaviour, IPlayerMovementController
{
	private PlayerParams playerParams;
	private Rigidbody2D rb;
	
	public float RunDamp = 0.15f;

	private Vector2 velocity;
	private Vector2 moveInput;
	private float jumpInput;

	private bool isGrounded;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		playerParams = GetComponent<PlayerParams>();
		
	}

	public void SetMoveInput(Vector2 input01)
	{
		moveInput = input01;
	}

	private bool _isJumpPressing;
	private int _numOfJump; //reset by the raycast on ground 
	
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

	public float jumpHoldTime;
	public LayerMask groundLayer;

	private void SetGround()
	{
		isGrounded = true;
		_currentGravity = 0;
		_numOfJump = playerParams.Stat.numberOfJump;
	}

	private void SetAirborne()
	{
		isGrounded = false;
		_currentCoyoteTime = playerParams.Stat.coyoteTime;
		_currentGravity = playerParams.Stat.gravity;
		
	}

	public Transform groundCheck;
	private float _currentGravity;
	private float _currentCoyoteTime;
	private float _velocityX;
	
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
			if (!isGrounded)
			{
				SetGround();
			}
		}
		else
		{
			if (isGrounded)
			{
				SetAirborne();	
			}
		}

		if (!isGrounded)
		{
			_currentGravity += playerParams.Stat.airBorneGravityIncreaseRate * Time.fixedDeltaTime; //lerp function required
			_currentCoyoteTime -= Time.fixedDeltaTime;
		}
		                       

 
		var targetVelocity = new Vector2(moveInput.x * playerParams.Stat.moveVelocity, rb.velocity.y - _currentGravity + jumpInput * playerParams.Stat.jumpVelocity);
		rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, targetVelocity.x, ref _velocityX, RunDamp, Mathf.Infinity, Time.fixedDeltaTime), targetVelocity.y);

	}

	
}
