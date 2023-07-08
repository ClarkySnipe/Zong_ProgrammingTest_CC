using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Sound[] MusicTracks;


    public static AudioManager AM_instance;

    private Sound MainMenuSound;
    private AudioSource MusicSource;

    private AudioListener AudioListener;

    private int TrackIndx = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (AM_instance == null)
            AM_instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        AudioListener = FindObjectOfType<Camera>().GetComponent<AudioListener>();

        foreach (Sound s in sounds)
        {
            if (s.source == null)
                s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = 1;

            s.source.playOnAwake = s.playOnAwake;
            s.source.loop = s.isLooping;

        }

        foreach (Sound s in MusicTracks)
        {
            if (s.source == null)
                s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = 1;

            s.source.playOnAwake = s.playOnAwake;
            s.source.loop = s.isLooping;

        }
    }

    public void Play(string name, int clipID)
    {
        Debug.Log("trying to play clip: " + name);
        foreach (Sound s in sounds)
        {
            if (s.name == name || s.ID == clipID)
            {
                if (!s.source.isPlaying)
                {
                    if (s.isMusic)
                        s.source.volume = GameManager.Instance.MusicVolume;
                    if (s.isSFX)
                        s.source.volume = GameManager.Instance.SFXVolume;

                    s.source.time = 0;
                    s.source.Play();
                    return;
                }
            }
        }
    }
    private void PlayMusic(string name, int clipID)
    {
        foreach (Sound s in MusicTracks)
        {
            if (s.name == name || s.ID == clipID)
            {
                if (!s.source.isPlaying)
                {
                    s.source.time = 0;
                    s.source.volume = 1;
                    s.source.Play();
                    MusicSource = s.source;
                    return;
                }
            }
        }
    }
    public void OnButtonClick()
    {
        Play("Button", 1);
    }

    public void PlayLevelMusic()
    {
        //Randomly choose a song from our MusicTrack List.
        TrackIndx = UnityEngine.Random.Range(0, MusicTracks.Length - 1);
        Debug.Log("Track Indx = " + TrackIndx);

        PlayMusic(MusicTracks[TrackIndx].name, MusicTracks[TrackIndx].ID);

    }
    public void PlayNextMusicTrack()
    {
        //Move onto next song - or move back to first index using modulo
        TrackIndx = (TrackIndx + 1) % MusicTracks.Length;

        Debug.Log("Moving onto next track - TrackIndx");
        PlayMusic(MusicTracks[TrackIndx].name, MusicTracks[TrackIndx].ID);
    }

    public void SetSoundVolumes()
    {
        foreach (Sound s in sounds)
        {
            if (s.source == null)
                s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            if (s.isSFX)
                s.source.volume = GameManager.Instance.SFXVolume;
            else if (s.isMusic)
                s.source.volume = GameManager.Instance.MusicVolume;

            s.source.pitch = 1;

            s.source.playOnAwake = s.playOnAwake;
            s.source.loop = s.isLooping;

        }

        foreach (Sound s in MusicTracks)
        {
            if (s.source == null)
                s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.source.volume = GameManager.Instance.MusicVolume;
            s.source.pitch = 1;

            s.source.playOnAwake = s.playOnAwake;
            s.source.loop = s.isLooping;

        }

        AudioListener.volume = GameManager.Instance.MasterVolume;
    }

}
