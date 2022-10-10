using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SecondLevelManager : MonoBehaviour
{
    [SerializeField] public GameObject Flexer;
    [SerializeField] public LevelModel CurrentLevelModel;
    [SerializeField] public SkinController BossSkin;
    public GameUI Gameui;
    private SecondGameController gameController;
    private SecondSpawnLogObjects LogObjects;
    public int Knives;
    public int Asses;
    static LevelModel BasicLevel = new LevelModel
    {
        LevelNumber = 0,
        IsBossLevel = false,

        LogMinRotationSpeed = -500f,
        LogMaxRotationSpeed = 500f,
        SmoothLogRotation = 10f,

        MinTimeRotation = 1f,
        MaxTimeRotation = 2.5f,
        IsLogRotating = true,

        Knives = 0,
        Asses = 0,
        LogShakingSpeed = 0f,

        KnifeCount = 5,
        SpawnKnifeDelay = 0,
    };

    public void SetLevelZero()
    {
        ResetLevelNumber();
        CurrentLevelModel = BasicLevel;
    }
    void CheckAndSetMaxLevel()
    {
        int CurrentLevel = PlayerPrefs.GetInt("LevelNumber");
        int MaxLevel = PlayerPrefs.GetInt("MaxLevel");

        if (CurrentLevel > MaxLevel)
        {
            PlayerPrefs.SetInt("MaxLevel", CurrentLevel);
            Gameui.UpdateLevelRecord();
        }
    }
    public void ResetLevelNumber()
    {
        PlayerPrefs.SetInt("LevelNumber", 0);
    }
    public int GetMaxLevel()
    {
        int MaxLevel = PlayerPrefs.GetInt("MaxLevel", 0);
        return MaxLevel;
    }
    void CheckAndSetMaxLvl()
    {
        int CurrentLevel = PlayerPrefs.GetInt("LevelNumber");
    }
    public void NextLevel()
    {
        CheckAndSetMaxLevel();

        LevelModel nextLevel = CurrentLevelModel;

        nextLevel.LevelNumber++;
        nextLevel.IsLogRotating = true;
        nextLevel.IsBossLevel = false;

        PlayerPrefs.SetInt("LevelNumber", nextLevel.LevelNumber);
        if (nextLevel.LevelNumber == 0)
        {
            gameController = FindObjectOfType<SecondGameController>();
            Knives = 2;
            Asses = 1;
            gameController.KnifeCount = 6;
        }
        if (nextLevel.LevelNumber == 1)
        {
            gameController = FindObjectOfType<SecondGameController>();
            Knives = 2;
            Asses = 0;
            gameController.KnifeCount = 6;
        }
        if (nextLevel.LevelNumber == 2)
        {
            gameController = FindObjectOfType<SecondGameController>();
            Knives = 0;
            Asses = 2;
            gameController.KnifeCount = 3;
        }
        if (nextLevel.LevelNumber % 3 == 0)
        {
            gameController = FindObjectOfType<SecondGameController>();
            //BossSkin.BossNum = 0;
            //SKIN CONTROLLER
            //
            //
            nextLevel.IsBossLevel = true;
            nextLevel.LogShakingSpeed = 0.001f;
            Knives = 0;
            Asses = 6;
            gameController.KnifeCount = 6;
        }
        if (nextLevel.LevelNumber % 6 == 0)
        {
            gameController = FindObjectOfType<SecondGameController>();
            //BossSkin.BossNum = 1;
            //SKIN CONTROLLER
            //
            //
            nextLevel.IsBossLevel = true;
            nextLevel.LogShakingSpeed = 0.08f;
            gameController.KnifeCount = 7;
            Knives = 3;
            Asses = 3;
        }
        if (nextLevel.LevelNumber % 9 == 0)
        {
            gameController = FindObjectOfType<SecondGameController>();
            Instantiate(Flexer);
            //BossSkin.BossNum = 0;
            //SKIN CONTROLLER
            //
            //
            nextLevel.IsBossLevel = true;
            nextLevel.LogShakingSpeed = 0.1f;
            gameController.KnifeCount = 9;
            Asses = 8;
        }

        if (nextLevel.LogMaxRotationSpeed > -1500)
            nextLevel.LogMaxRotationSpeed *= 1.01f;

        if (nextLevel.LogMinRotationSpeed < 1500)
            nextLevel.LogMinRotationSpeed *= 1.01f;

        if (nextLevel.MaxTimeRotation > 0.5f)
            nextLevel.MaxTimeRotation -= 0.01f;

        if (nextLevel.MinTimeRotation > 1f)
            nextLevel.MinTimeRotation -= 0.01f;

        CurrentLevelModel = nextLevel;
    }
}


