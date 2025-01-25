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
	}

	public void OnJump(InputAction.CallbackContext context)
	{
		playerController.SetJumpInput(context.ReadValueAsButton());
	}

	public void OnAbility(InputAction.CallbackContext context)
	{
		if (context.performed)
			playerAbilityController.OnBubbleButtonDown();
		if (context.canceled)
			playerAbilityController.OnBubbleButtonRelease();
	}
	
	public void OnGuard(InputAction.CallbackContext context)
	{
		if (context.performed)
			playerAbilityController.OnGuardButtonDown();
		if (context.canceled)
			playerAbilityController.OnGuardButtonRelease();
	}

	private void Update()
	{
		playerController.SetMoveInput(lastMoveInput);
		playerAbilityController.SetMoveInput(lastMoveInput);
	}
}