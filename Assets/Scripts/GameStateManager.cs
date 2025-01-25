using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum GameState
    {
        Start,
        Play,
        Pause,
        End
    }

    public static GameStateManager gameStateInstancce { get; private set; }
    public GameState currentState;

    public delegate void OnGameStateChange(GameState newState);
    public static event OnGameStateChange GameStateChanged;

    void Awake()
    {
        if (gameStateInstancce == null)
        {
            gameStateInstancce = this;
            DontDestroyOnLoad(gameObject);
            currentState = GameState.Start;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetGameState(GameState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            GameStateChanged?.Invoke(newState);
        }
    }
}
