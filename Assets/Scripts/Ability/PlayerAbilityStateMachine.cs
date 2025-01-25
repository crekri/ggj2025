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
		{
			state.InitState(this);
			state.enabled = false;
		}

		TransitTo(new PlayerAbilityIdleState.Config());
	}

	public void TransitTo<TConfig>(TConfig config) where TConfig : IStateConfig
	{
		if (currentState != null)
		{
			currentState.OnExit(false);
			currentState.enabled = false;
		}

		var newState = GetComponent<PlayerAbilityStateBehaviour<TConfig>>();
		currentState = newState;
		newState.OnEnter(config);
		currentState.enabled = true;

		Debug.Log($"Transitioning from {currentState.GetType().Name} to {newState.GetType().Name}");
	}

	public void Update()
	{
		currentState.OnUpdate();

		ammoController.GrantAmmo(ammoRecoveryRate * Time.deltaTime);
	}

	public void OnBubbleButtonDown() => currentState.OnBubbleButtonDown();
	public void OnBubbleButtonRelease() => currentState.OnBubbleButtonRelease();
	public void OnGuardButtonDown() => currentState.OnGuardButtonDown();
	public void OnGuardButtonRelease() => currentState.OnGuardButtonRelease();

	public Vector2 LastMoveInputUnit { get; private set; }

	public void SetMoveInput(Vector2 lastMoveInput)
	{
		if (lastMoveInput.magnitude > .2f)
			LastMoveInputUnit = lastMoveInput.normalized;
	}
}