using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public int ID;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.5f;

    [Range(0f, 3f)]
    public float pitch;

    //[HideInInspector]
    public AudioSource source;

    public bool isLooping;

    public bool playOnAwake = false;

    public bool isSFX = false;

    public bool isMusic = false;
}
