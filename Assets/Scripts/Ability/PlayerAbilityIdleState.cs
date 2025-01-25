using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAbilityIdleState : PlayerAbilityStateBehaviour
{
	[SerializeField] private PlayerAbilityBubbleChargeState bubbleChargeState;

	public override void OnBubbleButtonDown()
	{
		TransitionTo(bubbleChargeState);
	}
}