using UnityEngine;

public class PowerUpDoubleJumpHandler : MonoBehaviour , IPowerUpHandler
{
    public void OnCollect()
    {
        
        return;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        var isTriggerFound = col.GetComponentInParent<IPlayerController>();
        if (isTriggerFound != null)
        {
            isTriggerFound.OnPowerUpHit(this);
            OnCollect();
        }
    }
}