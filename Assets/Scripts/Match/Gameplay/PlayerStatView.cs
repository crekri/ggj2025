using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Match
{
	public class PlayerStatView : MonoBehaviour
	{
		[SerializeField] private Graphic[] playerColors;
		[SerializeField] private MatchPlayerConfigsSO configs;
		[SerializeField] private AnimatedText soapText;

		[SerializeField] private Image soapFill01;
		[SerializeField] private Image soapFill12;
		[SerializeField] private Gradient textColorGradient;
		[SerializeField] private PlayerAmmoIconView playerAmmoIconView;

		private PlayerController player;

		public void Bind(PlayerController player)
		{
			this.player = player;

			var config = configs.Get(player.Id);

			foreach (var graphic in playerColors)
			{
				graphic.color = config.Color;
			}
		}

		private void Update()
		{
			if (player == null)
				return;
			var soak = player.playerStateMachine.SoakController.Soak;
			var scale = Math.Max(2, 1.1f + soak * .5f);
			soapText.ChangeText(soak.ToString("P0"), startScale: scale, showDuration: 1000f);
			soapFill01.fillAmount = soak;
			soapFill12.fillAmount = soak - 1;
			textColorGradient.Evaluate(soak);
			soapText.Text.color = textColorGradient.Evaluate(Mathf.Clamp01(soak * .5f));
			playerAmmoIconView.SetValue(player.playerAbilityStateMachine.AmmoController.Ammo);
		}
	}
}