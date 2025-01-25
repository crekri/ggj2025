using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class PlayerAbilityStateBehaviour : MonoBehaviour, IAbilityInputHandler
{
	private PlayerAbilityStateMachine stateMachine;

	public virtual void OnEnter() { }
	public virtual void OnExit(bool isCancel) { }
	public virtual void OnUpdate() { }
	public virtual void OnBubbleButtonDown() { }
	public virtual void OnBubbleButtonRelease() { }

	public void InitState(PlayerAbilityStateMachine stateMachine)
	{
		this.stateMachine = stateMachine;
	}

	protected void TransitionTo(PlayerAbilityStateBehaviour newState)
	{
		stateMachine.TransitTo(newState);
	}
}