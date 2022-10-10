using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EZCameraShake;

[RequireComponent(typeof(GameUI))]
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    [SerializeField]
    private GameObject LogPrefab;
    [SerializeField]
    public LogRotation LogController;
    [SerializeField]
    public int KnifeCount;
    [Header("Knife Spawning")]
    [SerializeField]
    private Vector2 KnifeSpawnPosition;
    [SerializeField]
    public GameObject knifeObject;
    [SerializeField]
    private GameObject Bomb;
    [SerializeField]
    public Transform kniferot;
    [SerializeField]
    SkinController Skin;
    [SerializeField]
    AudioManager audioManager;
    
 


    [SerializeField]
    LevelManager levelManager;


    public GameUI GameUI { get; private set; }

    private void Awake()
    {
        Instantiate(LogPrefab);
        // destr = FindObjectOfType<Destructible>();
        audioManager = FindObjectOfType<AudioManager>();
        LogController = FindObjectOfType<LogRotation>();
        levelManager = FindObjectOfType<LevelManager>();
        levelManager.SetLevelZero();
        KnifeCount = Random.Range(2, 3);
        Instance = this;
        GameUI = GetComponent<GameUI>();
    }

    private void Start()
    {

        Skin = FindObjectOfType<SkinController>();
        Skin.SetLoverSkin();
        PlayerPrefs.SetInt("FirstOpen", 1);
        GameUI.SetInitialDisplayKnifeCount(KnifeCount);
        SpawnKnife();
    }

    public void OnSuccessufulKnifeHit()
    {
        if (KnifeCount > 0)
        {
            SpawnKnife();
        }
        else
        {
            StartCoroutine(NextLevel(true));
        }
    }

    private void SpawnKnife()
    {
        KnifeCount--;
        Instantiate(knifeObject, KnifeSpawnPosition,Quaternion.identity); 
    }
    public void WinCheck(bool win)
    {
        StartCoroutine(NextLevel(win));
    }




 
    public void StartFromZero()
    {
        LogController.ResetLogPos();
        levelManager.SetLevelZero();
        SpawnKnife();

    }

    public IEnumerator NextLevel(bool win)
    {
        yield return new WaitForSeconds(0.1f);
        if (win)
        {

            CameraShaker.Instance.ShakeOnce(3, 3, .1f, 1);
            audioManager.Play(Skin.Sound);
            if (GameObject.Find("RicardoFlexitAnim(Clone)"))
            {
                Destroy(GameObject.Find("RicardoFlexitAnim(Clone)"));
            }
            GameUI.DestroyKnifeCount();
 
            LogController.GetComponent<ParticleSystem>().Play();
            LogController.GetComponent<Animation>().Play("BrevnoEnd");
            yield return new WaitForSeconds(0.5f);
            Destroy(LogController.gameObject);
         
            levelManager.CurrentLevelModel.IsBossLevel = false;
            levelManager.CurrentLevelModel.IsLogRotating = false;
           
            win = false;
            yield return new WaitForSeconds(0.5f);
            Instantiate(LogPrefab);
            Skin.SetLoverSkin();




            levelManager.Knives = Random.Range(1, 3);
            levelManager.Asses = Random.Range(0, 9);
            levelManager.CurrentLevelModel.LogShakingSpeed = 0f;
            levelManager.NextLevel();
            Skin.SetLoverSkin();
            yield return new WaitForSeconds(0.2f);
            KnifeCount = Random.Range(3, 6);
            GameUI.SetInitialDisplayKnifeCount(KnifeCount);
            LogController = FindObjectOfType<LogRotation>();
            levelManager = FindObjectOfType<LevelManager>();

            GameUI.UpdateUiLevelValue();
            SpawnKnife();
            

            


        }
        else
        {
            CameraShaker.Instance.ShakeOnce(3, 3, .1f, 1);
            yield return new WaitForSeconds(0.2f);
            GameUI.ShowRestartButton();
            yield return new WaitForSeconds(0.2f);
            audioManager.Play("WhatAHell");
        }
    }
    public void RestartTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        Time.timeScale = 1f;
    }
}
