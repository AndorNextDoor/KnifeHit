using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    Knife knife;

    public void Resume()
    {
        Time.timeScale = 1f;
        knife.GetComponent<Knife>().gameObject.SetActive(true);
    }
    public void Pause()
    {
        knife = FindObjectOfType<Knife>();
        knife.GetComponent<Knife>().gameObject.SetActive(false);
        Time.timeScale = 0f;
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
