using Bubble;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField]public PlayerTrapState playerTrapState;
    public PlayerState CurrentState { get; private set; }

    public void Awake()
    {
        foreach (var state in GetComponents<PlayerState>())
        {
            state.Init(this);
        }
		
        TransitTo(GetComponent<PlayerFreeState>());
    }

    public void TransitTo(PlayerState playerState)
    {
        if (CurrentState != null)
        {
            CurrentState.OnExit();
        }

        CurrentState = playerState;
        CurrentState.OnEnter();
    }

    public void FixedUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.MyFixedUpdate();
        }
    }
	
    public void OnBubbleHit(BubbleHitInfo info)
    {
        if (info.IsTrap)
        {
            TransitTo(playerTrapState);
        }
    }
}