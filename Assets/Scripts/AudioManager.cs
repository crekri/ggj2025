using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManagerInstance { get; private set; }

    public AudioSource backgroundMusicSource;
    public AudioSource sfxSource;

    void Awake()
    {
        if (audioManagerInstance == null)
        {
            audioManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
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
                break;
            case GameStateManager.GameState.Play:
                break;
            case GameStateManager.GameState.Pause:
                break;
            case GameStateManager.GameState.End:
                break;
        }
    }

    public void PlayBackgroundMusic(AudioClip musicClip)
    {
        if (backgroundMusicSource.clip != musicClip)
        {
            backgroundMusicSource.clip = musicClip;
            backgroundMusicSource.Play();
        }
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        sfxSource.PlayOneShot(sfxClip);
    }

    public void PlaySFX(AudioClip sfxClip, System.Action callback)
    {
        StartCoroutine(PlaySFXWithCallback(sfxClip, callback));
    }

    private IEnumerator PlaySFXWithCallback(AudioClip sfxClip, System.Action callback)
    {
        sfxSource.PlayOneShot(sfxClip);
        yield return new WaitWhile(() => sfxSource.isPlaying);
        callback?.Invoke();
    }
}
