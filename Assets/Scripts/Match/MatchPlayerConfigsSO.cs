using System;
using UnityEngine;

namespace Match
{
	[Serializable]
	public class MatchPlayerConfig
	{
		public Color Color;
	}

	[CreateAssetMenu(menuName = "Create MatchPlayerConfigsSO", fileName = "MatchPlayerConfigsSO", order = 0)]
	public class MatchPlayerConfigsSO : ScriptableObject
	{
		[SerializeField] private MatchPlayerConfig[] PlayerConfigs;

		public MatchPlayerConfig Get(int playerId) => PlayerConfigs[playerId];
	}
}