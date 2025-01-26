using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

		[SerializeField] private PlayerInputManager inputManager;

		public static MatchStateGame Instance { get; private set; }

		private HashSet<PlayerController> alivePlayers = new();

		private void Awake()
		{
			Instance = this;
		}

		public override void OnEnter()
		{
			inputManager.DisableJoining();
			gameUi.SetActive(true);

			var players = this.matchPlayerManager.Players;
			for (var id = 0; id < players.Count; id++)
			{
				var player = players[id];
				player.Init(spawnPoints.GetNonOccupiedSpawnPoint().position, id, configs.Get(id));
			}

			alivePlayers.Clear();
			alivePlayers.UnionWith(players);

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

		public override void OnUpdate() { }

		private bool isGameEnded = false;

		public void OnPlayerKilled(PlayerController playerController)
		{
			if (isGameEnded)
				return;

			alivePlayers.Remove(playerController);
			if (alivePlayers.Count > 1)
			{
				infoText.ChangeText("<color=red>" + playerController.Config.Name + " ELIMINATED!</color>", .8f, 1f, 1f);
			}
			else
			{
				StartCoroutine(IAnnounceWinner());
			}
		}

		public IEnumerator IAnnounceWinner()
		{
			isGameEnded = true;

			var finalPlayer = alivePlayers.First();
			var text = $"Winner is\n<color=red>{finalPlayer.Config.Name}</color>";
			infoText.ChangeText(text, .8f, 1f, 3f);
			yield return new WaitForSeconds(3f);

			//Restart current scene
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}