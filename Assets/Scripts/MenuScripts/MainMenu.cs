using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    AudioManager audioManager;
    public void PlayGame()
    {
        StartCoroutine(Play());
    }
    IEnumerator Play()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("CmonLetsGo");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
