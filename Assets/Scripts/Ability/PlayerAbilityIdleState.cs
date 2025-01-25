using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAbilityIdleState : PlayerAbilityStateBehaviour<PlayerAbilityIdleState.Config>
{
	public class Config : IStateConfig
	{
		public Config(float stateTransitionCooldown = 0)
		{
			StateTransitionCooldown = stateTransitionCooldown;
		}

		public float StateTransitionCooldown { get; }
	}

	[SerializeField] private PlayerAbilityBubbleChargeState bubbleChargeState;

	private float transitionTimer;

	public override void OnEnter(Config config)
	{
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