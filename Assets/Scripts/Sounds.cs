using UnityEngine.Audio;
using UnityEngine;
[System.Serializable]
public class Sounds
    {
    public string Name;
    
    public AudioClip clip;
    [Range(0f, 1f)]
    public float Volume;
    [Range(0.1f, 3f)]
    public float Pitch;
    [Range(0f, 256f)]
    public int Priority;
    public AudioMixerGroup mixer;
    public bool loop;
    [HideInInspector]
    public AudioSource source;
    }
