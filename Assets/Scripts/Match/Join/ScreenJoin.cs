using System.Collections.Generic;
using UnityEngine;

namespace Match
{
	public class ScreenJoin : MonoBehaviour
	{
		[SerializeField] private MatchStateMachine matchStateMachine;
		[SerializeField] private MatchPlayerManager playerManager;
		[SerializeField] private Transform playerViewsParent;
		[SerializeField] private ScreenJoinPlayerView playerViewPrefab;

		[SerializeField] private GameObject joinButton;

		private List<ScreenJoinPlayerView> playerViews = new();

		private void Awake()
		{
			matchStateMachine.TransitionTo(matchStateMachine.GetComponentInChildren<MatchStateJoin>());
			for (int i = 0; i < MatchPlayerManager.MaxPlayers; i++)
			{
				var playerView = Instantiate(playerViewPrefab, playerViewsParent);
				playerView.Setup(i);
				playerView.gameObject.SetActive(false);
				playerViews.Add(playerView);
			}
		}

		private void Update()
		{
			var playerCount = playerManager.Players.Count;
			for (int i = 0; i < MatchPlayerManager.MaxPlayers; i++)
			{
				playerViews[i].gameObject.SetActive(i < playerCount);
			}

			joinButton.gameObject.SetActive(playerCount >= 1);
		}
	}
}