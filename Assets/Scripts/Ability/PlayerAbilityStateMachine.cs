using Unity.VisualScripting;
using UnityEngine;

public class PlayerAbilityStateMachine : MonoBehaviour, IAbilityInputHandler
{
	[SerializeField] private PlayerAmmoController ammoController;
	[SerializeField] private PlayerAbilityIdleState idleState;
	private PlayerAbilityStateBehaviour currentState;

	[SerializeField] private float ammoRecoveryRate = 3f;

	private void Awake()
	{
		foreach (var state in GetComponentsInChildren<PlayerAbilityStateBehaviour>())
			state.InitState(this);
		TransitTo(idleState);
	}

	public void TransitTo(PlayerAbilityStateBehaviour newState)
	{
		if (currentState != null) currentState.OnExit(false);
		currentState = newState;
		currentState.OnEnter();
		Debug.Log($"Transitioning from {currentState.GetType().Name} to {newState.GetType().Name}");
	}

	public void Update()
	{
		currentState.OnUpdate();

		ammoController.GrantAmmo(ammoRecoveryRate * Time.deltaTime);
	}

	public void OnBubbleButtonDown() => currentState.OnBubbleButtonDown();
	public void OnBubbleButtonRelease() => currentState.OnBubbleButtonRelease();
}