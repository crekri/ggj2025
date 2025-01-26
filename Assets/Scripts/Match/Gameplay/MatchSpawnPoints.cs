using System.Collections.Generic;
using UnityEngine;

namespace Match
{
	public class MatchSpawnPoints : MonoBehaviour
	{
		[SerializeField] private Transform[] spawnPoints;
		private List<Transform> availablePoints = new();

		public Transform GetNonOccupiedSpawnPoint()
		{
			if(availablePoints.Count == 0)
				availablePoints.AddRange(spawnPoints);

			var randomId = Random.Range(0, availablePoints.Count);
			var item = availablePoints[randomId];
			availablePoints.Remove(item);
			return item;
		}
	}
}