using UnityEngine;

public class TriggerElimation : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D col)
    {
        var colParent = col.GetComponentInParent<IPlayerController>();
        if (colParent != null)
        {
            Destroy(col.transform.parent.gameObject);
        }
    }
	
}