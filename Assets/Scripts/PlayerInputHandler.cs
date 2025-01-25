using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovementController))]
public class PlayerInputHandler : MonoBehaviour
{
	private PlayerMovementController playerController;

	private void Awake()
	{
		playerController = GetComponent<PlayerMovementController>();
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		playerController.SetMoveInput(context.ReadValue<Vector2>());
	}

	public void OnJump(InputAction.CallbackContext context)
	{
		if (context.performed) playerController.SetJump();
	}
}