using System;
using UnityEngine;

namespace Match
{
	public class MatchStateMachine : MonoBehaviour
	{
		[SerializeField] private AMatchStateBehaviour initialState;
		private AMatchStateBehaviour currentState;
		
		private void Awake()
		{
			foreach (var state in GetComponentsInChildren<AMatchStateBehaviour>())
				state.InitState(this);
			TransitionTo(initialState);
		}

		public void TransitionTo(AMatchStateBehaviour newState)
		{
			if (newState == null)
				return;

			if (currentState != null)
				currentState.OnExit();

			currentState = newState;
			currentState.OnEnter();
		}

		private void Update()
		{
			currentState.OnUpdate();
		}
	}

	public abstract class AMatchStateBehaviour : MonoBehaviour
	{
		protected MatchStateMachine StateMachine { get; private set; }

		public void InitState(MatchStateMachine stateMachine)
		{
			this.StateMachine = stateMachine;
		}

		public virtual void OnEnter() { }
		public virtual void OnExit() { }
		public virtual void OnUpdate() { }
	}
}