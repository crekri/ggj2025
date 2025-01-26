using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Match
{
	public class MatchStateGame : AMatchStateBehaviour
	{
		[SerializeField] private MatchSpawnPoints spawnPoints;
		[SerializeField] private MatchPlayerManager matchPlayerManager;
		[SerializeField] private GameObject gameUi;

		[SerializeField] private AnimatedText infoText;

		public override void OnEnter()
		{
			gameUi.SetActive(true);

			var players = this.matchPlayerManager.Players;
			foreach (var player in players)
				player.Respawn(spawnPoints.GetNonOccupiedSpawnPoint().position);

			StartCoroutine(IEnter());
		}

		private IEnumerator IEnter()
		{
			infoText.ChangeText("Ready...");
			yield return new WaitForSeconds(1.5f);
			infoText.ChangeText("BRAWLBULE!", targetScale: 2, showDuration: .5f);

			foreach (var player in this.matchPlayerManager.Players)
				player.playerStateMachine.TransitTo(player.playerStateMachine.PlayerFreeState);
		}

		public override void OnExit()
		{
			gameUi.SetActive(false);
		}
	}
}