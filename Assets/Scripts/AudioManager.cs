using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;

    public static AudioManager instance;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
        foreach (Sounds s in sounds)
        {
            s.source =  gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.priority = s.Priority;
            s.source.volume = s.Volume;
            s.source.pitch = s.Pitch;
            s.source.outputAudioMixerGroup = s.mixer;
            s.source.loop = s.loop;
        }
       
    }
    public void Start()
    {
        Play("Bg");
    }
    public void Play(string Name)
    {
     Sounds s  =  Array.Find(sounds, sound => sound.Name == Name);
        if (s == null)
            return;
        s.source.Play(); 
    }
}
