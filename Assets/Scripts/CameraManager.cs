using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera defaultCamera;
    public CinemachineVirtualCamera playCamera;

    void Start()
    {
        defaultCamera.Priority = 0;
        playCamera.Priority = 1;
        GameStateManager.GameStateChanged += OnGameStateChanged;
    }

    void OnDestroy()
    {
        GameStateManager.GameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameStateManager.GameState newState)
    {
        switch (newState)
        {
            case GameStateManager.GameState.Start:
                defaultCamera.Priority = 0;
                playCamera.Priority = 1;
                break;
            case GameStateManager.GameState.Pause:
                break;
            case GameStateManager.GameState.End:
                defaultCamera.Priority = 1;
                playCamera.Priority = 0;
                break;
            case GameStateManager.GameState.Play:
                defaultCamera.Priority = 0;
                playCamera.Priority = 1;
                break;
        }
    }
}
