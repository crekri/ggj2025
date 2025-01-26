using System.Collections.Generic;
using UnityEngine;

namespace Match
{
	public class PlayerStatViewGroup : MonoBehaviour
	{
		[SerializeField] private Transform playerStatViewParent;
		[SerializeField] private PlayerStatView viewPrefab;
		
		public void Init(IEnumerable<PlayerController> players)
		{
			foreach (var player in players)
			{
				var view = Instantiate(viewPrefab, playerStatViewParent);
				view.Bind(player);
			}
		}
	}
}