using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAbilityIdleState : PlayerAbilityStateBehaviour<PlayerAbilityIdleState.Config>
{
	public class Config : IStateConfig
	{
		public Config(float stateTransitionCooldown = 0, PlayerAbilityStateBehaviour fromState = null)
		{
			StateTransitionCooldown = stateTransitionCooldown;
			FromStateNullable = fromState;
		}

		public PlayerAbilityStateBehaviour FromStateNullable { get; }
		public float StateTransitionCooldown { get; }
	}

	public PlayerAbilityStateBehaviour FromStateNullable { get; private set; }
	[SerializeField] private PlayerAbilityBubbleChargeState bubbleChargeState;

	private float transitionTimer;

	public override void OnEnter(Config config)
	{
		FromStateNullable = config.FromStateNullable;
		transitionTimer = config.StateTransitionCooldown;
	}

	public override void OnUpdate()
	{
		transitionTimer -= Time.deltaTime;
	}

	bool CanTransition => transitionTimer <= 0;

	public override void OnBubbleButtonDown()
	{
		if (!CanTransition)
			return;

		StateMachine.TransitTo(new PlayerAbilityBubbleChargeState.Config());
	}

	public override void OnGuardButtonDown()
	{
		if (!CanTransition)
			return;

		StateMachine.TransitTo(new PlayerAbilityGuardState.Config());
	}
}