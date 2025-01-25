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

	public void OnMove(InputAction.CallbackContext context)
	{
		playerController.SetMoveInput(context.ReadValue<Vector2>());
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
}