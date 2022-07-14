using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public Slider SFXslider;
    public AudioMixer SFXaudiomixer;
    public Slider Musicslider;
    public AudioMixer Musicaudiomixer;


    public void SetSFXVolume(float volume)
    {
        SFXaudiomixer.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat("SFX", volume);
        
    }
    public void SetMusicVolume(float volume)
    {
        Musicaudiomixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("Music", volume);

    }
    public void Start()
    {
        SFXslider.value = PlayerPrefs.GetFloat("SFX");
        SFXaudiomixer.SetFloat("SFXVolume", SFXslider.value);
        //
        Musicslider.value = PlayerPrefs.GetFloat("Music");
        Musicaudiomixer.SetFloat("MusicVolume", Musicslider.value);
    }

}
