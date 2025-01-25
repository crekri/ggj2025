using UnityEngine;

public class PlayerTrapState : PlayerState
{
    //Movement control 
    [SerializeField] private PlayerParams playerParams;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerFreeState playerFreeState;
    [SerializeField] private Transform flipTransform;
    private Vector2 velocity;
    private Vector2 moveInput;
    private float jumpInput;

    private bool _isJumpPressing;
    private int _numOfJump; //reset by the raycast on ground 
    private bool _isGrounded;
    private float _currentGravity;
    private float _velocityX;

    private float _currentLifeTime;
	
    public override void OnEnter()
    {
        _currentLifeTime = playerParams.Stat.trapTimer;
    }

    public override void OnExit()
    {
		
    }
	

    public override void MyFixedUpdate()
    {
        if (_currentLifeTime > 0)
        {
            _currentLifeTime -= Time.deltaTime;
            
            if (Mathf.Abs(moveInput.x) > .5f)
            {
                IsFacingRight = moveInput.x > 0;
                flipTransform.localScale = new Vector3(IsFacingRight ? 1 : -1, 1, 1);
            }
        }
        else
        {
            TransitTo(playerFreeState);
        }
    }

    public override void SetMoveInput(Vector2 inputVector)
    {
        moveInput = inputVector;
    }

    public override void SetJumpInput(bool isPressed)
    {
        if (isPressed)
        {
            if (!_isJumpPressing)
            {
                _currentLifeTime -= playerParams.Stat.reduceTrapPerClick;
            }
        }
    }
}