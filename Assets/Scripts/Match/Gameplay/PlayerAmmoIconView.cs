using UnityEngine;

namespace Match
{
	public class PlayerAmmoIconView : MonoBehaviour
	{
		[SerializeField] private Transform[] ammoIcons;

		public void SetValue(float newAmount)
		{
			for (var i = 0; i < ammoIcons.Length; i++)
			{
				ammoIcons[i].gameObject.SetActive(i <= newAmount + 1);
			}
		}
	}
}