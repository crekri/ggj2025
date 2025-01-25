using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AudioManagerInstance { get; private set; }

    public AudioSource backgroundMusicSource;
    public AudioSource sfxSourcePrefab;

    private readonly List<AudioSource> sfxSources = new();

    void Awake()
    {
        if (AudioManagerInstance == null)
        {
            AudioManagerInstance = this;
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
        AudioSource sfxSource = GetAvailableSFXSource();
        sfxSource.PlayOneShot(sfxClip);
    }

    public void PlaySFX(AudioClip sfxClip, System.Action callback)
    {
        StartCoroutine(PlaySFXWithCallback(sfxClip, callback));
    }

    private IEnumerator PlaySFXWithCallback(AudioClip sfxClip, System.Action callback)
    {
        AudioSource sfxSource = GetAvailableSFXSource();
        sfxSource.PlayOneShot(sfxClip);
        yield return new WaitWhile(() => sfxSource.isPlaying);
        callback?.Invoke();
    }

    private AudioSource GetAvailableSFXSource()
    {
        foreach (AudioSource source in sfxSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }

        AudioSource newSource = Instantiate(sfxSourcePrefab, transform);
        sfxSources.Add(newSource);
        return newSource;
    }

    public void StopAllSFX()
    {
        foreach (AudioSource source in sfxSources)
        {
            source.Stop();
        }
    }

    public void StopBackgroundMusic()
    {
        backgroundMusicSource.Stop();
    }

    public void StopSFX(AudioSource sfxSource)
    {
        if (sfxSources.Contains(sfxSource))
        {
            sfxSource.Stop();
        }
    }
}
