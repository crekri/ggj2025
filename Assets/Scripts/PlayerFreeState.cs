using UnityEngine;

public class PlayerFreeState : PlayerState
{
    public override void OnEnter()
    {
        moveInput = Vector2.zero;
    }

    public override void OnExit()
    {
        return;
    }

    public void ApplyKnockback(Vector2 lastInputUnit, float guardHitPower)
    {
        _currentFreezeTime = playerParams.Stat.baseStunTime;
        var shootDir = new Vector2(lastInputUnit.x, 0);
        rb.AddForce(shootDir * guardHitPower * playerParams.Stat.knockBackMult, ForceMode2D.Impulse);
    }

    private float _currentFreezeTime;
    
    //Movement control 
    [SerializeField] private PlayerParams playerParams;
    [SerializeField] private Rigidbody2D rb;
	
    private float jumpHoldTime;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform flipTransform;
    private Vector2 velocity;
    private Vector2 moveInput;
    private float jumpInput;

    private bool _isJumpPressing;
    private int _numOfJump; //reset by the raycast on ground 
    private bool _isGrounded;
    private float _currentGravity;
    private float _velocityX;

	
	
    public override void SetMoveInput(Vector2 input01)
    {
        if(_currentFreezeTime > 0) return;
        moveInput = input01;
        
    }

	
    public override void SetJumpInput(bool isPressed)
    {
        if(_currentFreezeTime > 0) return;
        if (isPressed)
        {
            if (_numOfJump <= 0)
            {
                return;
            }

            if (!_isJumpPressing)
            {
                _numOfJump-=1;
            }

            _isJumpPressing = true;
        }
        else
        {
            _isJumpPressing = false;
            jumpHoldTime = 0;
            jumpInput = 0;
        }

        if (isPressed)
        {
			
            rb.velocity = new Vector2(rb.velocity.x, playerParams.Stat.jumpVelocity);
        }
    }

    public override void SetAnyKeyDown()
    {
        return;
    }


    private void SetGround()
    {
        _isGrounded = true;
        _currentGravity = 0;
        _numOfJump = playerParams.Stat.numberOfJump;
    }

    private void SetAirborne()
    {
        _isGrounded = false;
        _currentGravity = playerParams.Stat.gravity;
		
    }

    public override void MyFixedUpdate()
    {
        if (_isJumpPressing)
        {
            jumpHoldTime += Time.deltaTime;
            if (jumpHoldTime < playerParams.Stat.jumpHoldTime)
            {
                jumpInput += playerParams.Stat.jumpHoldStrengthRate * Time.fixedDeltaTime; //lerp required
            }
            else
            {
                jumpInput = 0;
            }
        }
		
        if (Physics2D.OverlapCircle(groundCheck.position, .1f, groundLayer))
        {
            if (!_isGrounded)
            {
                SetGround();
            }
        }
        else
        {
            if (_isGrounded)
            {
                SetAirborne();	
            }
        }


        if (!_isGrounded)
        {
            _currentGravity += playerParams.Stat.airBorneGravityIncreaseRate * Time.fixedDeltaTime; //lerp function required
            
        }

        if (_currentFreezeTime > 0)
        {
            _currentFreezeTime -= Time.fixedDeltaTime;
        }
        else
        {
            var targetVelocity = new Vector2(moveInput.x * playerParams.Stat.moveVelocity, rb.velocity.y - _currentGravity + jumpInput * playerParams.Stat.jumpVelocity);
        
            rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, targetVelocity.x, ref _velocityX, playerParams.Stat.runDamp, Mathf.Infinity, Time.fixedDeltaTime), targetVelocity.y);
        
            if (Mathf.Abs(moveInput.x) > .5f)
            {
                IsFacingRight = rb.velocity.x > 0;
                flipTransform.localScale = new Vector3(IsFacingRight ? 1 : -1, 1, 1);
            }
        }

        
    }

}