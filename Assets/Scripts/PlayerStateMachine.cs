using Bubble;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField]private PlayerTrapState playerTrapState;
    [SerializeField]private PlayerSoakController playerSoakController;
    [SerializeField]private PlayerParams playerParams;
    public PlayerState CurrentState { get; private set; }
    public PlayerParams PlayerParams { get; private set; }

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

    [SerializeField] private AnimationCurve playerTrapRatio; 
    public void OnBubbleHit(BubbleHitInfo info)
    {
        playerSoakController.AddSoak(info.SoapAmount);
        var currentAmount = playerSoakController.Soak;

        if (currentAmount >= 1f)
        {
            bool isTrap = Random.Range(0, 1f) <= playerTrapRatio.Evaluate(currentAmount);
            if (isTrap)
            {
                TransitTo(playerTrapState);    
            }
        }
    }

    public void OnGuardHit(Vector2 lastInputUnit, float guardHitPower)
    {
        
        var freeState = GetComponent<PlayerFreeState>();
        if (CurrentState == freeState)
        {
            freeState.ApplyKnockback(lastInputUnit, guardHitPower);
        }
        else if(CurrentState == playerTrapState)
        {
            Debug.Log($"Destroy{playerParams.gameObject.name}");
            Destroy(playerParams.gameObject);
        }
    }
}