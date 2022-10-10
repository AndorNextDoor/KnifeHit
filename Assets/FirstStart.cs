using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStart : MonoBehaviour
{
    AudioManager audioManager;
    void Start()
    {
        if(PlayerPrefs.GetInt("FirstStart") == 0)
        {
            audioManager = FindObjectOfType<AudioManager>();
            audioManager.Play("WithoutFI");
            PlayerPrefs.SetInt("FirstStart", 1);
        }
    }
}
