using UnityEngine;

public class AmmoView : MonoBehaviour
{
	[SerializeField] private Transform bigBubbleRequirementBar;
	[SerializeField] private Transform ammoBar;
	[SerializeField] private PlayerAbilityController playerAbilityController;

	private void Update()
	{
		bigBubbleRequirementBar.transform.localScale = new Vector3(playerAbilityController.BigBubbleCost, 1, 1);
		ammoBar.transform.localScale = new Vector3(playerAbilityController.Ammo01, 1, 1);
	}
}