using System;
using System.Collections;
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
		[SerializeField] private AnimatedText roundText;

		[SerializeField] private PlayerStatViewGroup statViewGroup;
		[SerializeField] private MatchPlayerConfigsSO configs;
		
		public override void OnEnter()
		{
			gameUi.SetActive(true);

			var players = this.matchPlayerManager.Players;
			for (var id = 0; id < players.Count; id++)
			{
				var player = players[id];
				player.Init(spawnPoints.GetNonOccupiedSpawnPoint().position, id, configs.Get(id));
			}

			statViewGroup.Init(matchPlayerManager.Players);
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