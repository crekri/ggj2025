using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public interface IStateConfig { }

public abstract class PlayerAbilityStateBehaviour<TConfig> : PlayerAbilityStateBehaviour
	where TConfig : IStateConfig
{
	public virtual void OnEnter(TConfig config) { }
}

public abstract class PlayerAbilityStateBehaviour : MonoBehaviour, IAbilityInputHandler
{
	public PlayerAbilityStateMachine StateMachine { get; private set; }

	public virtual void OnExit(bool isCancel) { }
	public virtual void OnUpdate() { }
	public virtual void OnBubbleButtonDown() { }
	public virtual void OnBubbleButtonRelease() { }
	public virtual void OnGuardButtonDown() { }
	public virtual void OnGuardButtonRelease() { }

	public void InitState(PlayerAbilityStateMachine stateMachine)
	{
		this.StateMachine = stateMachine;
	}
}