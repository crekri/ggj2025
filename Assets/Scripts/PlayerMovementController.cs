using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovementController : MonoBehaviour
{
	private Rigidbody2D rb;

	public float RunSpeed = 10f;
	public float RunDamp = 0.15f;
	public float JumpVelocity = 10f;
	
	private Vector2 velocity;
	private Vector2 moveInput;


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
		if(isPressed)
			rb.velocity = new Vector2(rb.velocity.x, JumpVelocity);
	}

	private void FixedUpdate()
	{
		var targetVelocity = new Vector2(moveInput.x * RunSpeed, rb.velocity.y);
		rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, RunDamp, Mathf.Infinity, Time.fixedDeltaTime);
	}
}