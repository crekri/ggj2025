using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Match
{
	public class MatchPlayerManager : MonoBehaviour
	{
		public IReadOnlyList<PlayerController> Players => players;

		public const int MaxPlayers = 4;

		private readonly List<PlayerController> players = new();

		public void OnPlayerJoined(PlayerInput playerInput)
		{
			players.Add(playerInput.GetComponent<PlayerController>());
		}

		public void OnPlayerLeft(PlayerInput playerInput)
		{
			players.Remove(playerInput.GetComponent<PlayerController>());
		}
	}
}