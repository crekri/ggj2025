using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public bool playOnStart = false;
    public float volume = 1.0f;
    public bool loop = false; 
    public enum SFXType
    {
        Random,
        All
    }
    public SFXType playMethod = SFXType.All;

    void Start()
    {
        if (playOnStart)
        {
            PlaySFX();
        }
    }

    public void PlaySFX()
    {
        if (audioClips.Count == 0)
        {
            Debug.LogWarning("No audio clips provided");
            return;
        }
        else if (audioClips.Count == 1)
        {
            LoadSFX(audioClips[0], volume, loop);
        }
        else
        {
            switch (playMethod)
            {
                case SFXType.Random:
                    PlayRandomSFX(audioClips, volume, loop);
                    break;
                case SFXType.All:
                    PlayAllSFX(audioClips, volume, loop);
                    break;
            }
        }
    }
    public void LoadSFX(AudioClip sfxClip, float volume, bool loop)
    {
        AudioSource sfxSource = GetAvailableAudioSource();
        sfxSource.clip = sfxClip;
        sfxSource.loop = loop;
        sfxSource.volume = volume;
        sfxSource.Play();
    }

    public void PlayRandomSFX(List<AudioClip> sfxClips, float volume, bool loop)
    {
        if (sfxClips.Count == 0)
        {
            Debug.LogWarning("No audio clips provided");
            return;
        }

        int randomIndex = Random.Range(0, sfxClips.Count);
        AudioClip randomClip = sfxClips[randomIndex];
        LoadSFX(randomClip, volume, loop);
    }

    public void PlayAllSFX(List<AudioClip> sfxClips, float volume, bool loop)
    {
        if (sfxClips.Count == 0)
        {
            Debug.LogWarning("No audio clips provided");
            return;
        }

        for (int i = 0; i < sfxClips.Count; i++)
        {
            LoadSFX(sfxClips[i], volume, loop);
        }
    }

    private AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource source in GetComponents<AudioSource>())
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }

        return gameObject.AddComponent<AudioSource>();
    }

    public void StopAllSFX()
    {
        foreach (AudioSource source in GetComponents<AudioSource>())
        {
            source.Stop();
        }
    }

    public void StopSFX(int sourceIndex)
    {
        AudioSource[] sources = GetComponents<AudioSource>();
        if (sourceIndex < 0 || sourceIndex >= sources.Length)
        {
            Debug.LogWarning("Invalid audio source index");
            return;
        }

        sources[sourceIndex].Stop();
    }
}
