using Unity.VisualScripting;
using UnityEngine;

public class PlayerAbilityStateMachine : MonoBehaviour, IAbilityInputHandler
{
	public PlayerAmmoController AmmoController => ammoController;
	[SerializeField] private PlayerAmmoController ammoController;
	[SerializeField] private PlayerAbilityIdleState idleState;
	public PlayerAbilityStateBehaviour CurrentState { get; private set; }

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
		if (CurrentState != null)
		{
			CurrentState.OnExit(false);
			CurrentState.enabled = false;
		}

		var newState = GetComponent<PlayerAbilityStateBehaviour<TConfig>>();
		CurrentState = newState;
		newState.OnEnter(config);
		CurrentState.enabled = true;

		Debug.Log($"Transitioning from {CurrentState.GetType().Name} to {newState.GetType().Name}");
	}

	public void Update()
	{
		CurrentState.OnUpdate();

		ammoController.GrantAmmo(ammoRecoveryRate * Time.deltaTime);
	}

	public void OnBubbleButtonDown() => CurrentState.OnBubbleButtonDown();
	public void OnBubbleButtonRelease() => CurrentState.OnBubbleButtonRelease();
	public void OnGuardButtonDown() => CurrentState.OnGuardButtonDown();
	public void OnGuardButtonRelease() => CurrentState.OnGuardButtonRelease();

	public Vector2 LastMoveInputUnit { get; private set; }

	public void SetMoveInput(Vector2 lastMoveInput)
	{
		if (lastMoveInput.magnitude > .2f)
			LastMoveInputUnit = lastMoveInput.normalized;
	}
}