using UnityEngine;
using UnityEngine.Serialization;

public class AmmoView : MonoBehaviour
{
	[SerializeField] private Transform ammoBar;
	[SerializeField] private PlayerAmmoController ammoController;[SerializeField] private PlayerAbilityBubbleChargeState bubbleChargeController;

	private void Update()
	{
		ammoBar.transform.localScale = new Vector3(ammoController.Ammo01, 1, 1);
	}
}