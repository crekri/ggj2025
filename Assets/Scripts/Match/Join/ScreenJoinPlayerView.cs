using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Match
{
	public class ScreenJoinPlayerView : MonoBehaviour
	{
		[SerializeField] private MatchPlayerConfigsSO playerConfigs;
		[SerializeField] private Image colorImage;
		[SerializeField] private TextMeshProUGUI playerIdText;

		public void Setup(int playerId)
		{
			playerIdText.text = "Player " + (playerId + 1).ToString();
			colorImage.color = playerConfigs.Get(playerId).Color;
		}
	}
}