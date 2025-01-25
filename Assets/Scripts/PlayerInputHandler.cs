using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
	private IPlayerMovementController playerController;
	private PlayerAbilityController playerAbilityController;

	private void Awake()
	{
		playerController = GetComponent<IPlayerMovementController>();
		playerAbilityController = GetComponent<PlayerAbilityController>();
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
		playerAbilityController.SetInput(context.ReadValueAsButton());
	}
}