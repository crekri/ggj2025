using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
	private IPlayerMovementController playerController;
	[SerializeField] private PlayerAbilityStateMachine playerAbilityController;

	private void Awake()
	{
		playerController = GetComponent<IPlayerMovementController>();
	}

	private Vector2 lastMoveInput;
	public void OnMove(InputAction.CallbackContext context)
	{
		lastMoveInput = context.ReadValue<Vector2>();
		OnAnyKeyDown(lastMoveInput != Vector2.zero);
	}

	public void OnJump(InputAction.CallbackContext context)
	{
		playerController.SetJumpInput(context.ReadValueAsButton());
		OnAnyKeyDown(context.ReadValueAsButton());
	}

	public void OnAbility(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			playerAbilityController.OnBubbleButtonDown();
			OnAnyKeyDown(true);
		}
			
		if (context.canceled)
			playerAbilityController.OnBubbleButtonRelease();
	}
	
	public void OnGuard(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			playerAbilityController.OnGuardButtonDown();
			OnAnyKeyDown(true);
		}

		
		if (context.canceled)
			playerAbilityController.OnGuardButtonRelease();
	}

	public void OnAnyKeyDown(bool result)
	{
		if (result)
		{
			playerController.SetAnyKeyDown();
		}
	}

	private void Update()
	{
		playerController.SetMoveInput(lastMoveInput);
		playerAbilityController.SetMoveInput(lastMoveInput);
	}
}