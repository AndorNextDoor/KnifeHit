using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LevelManager : MonoBehaviour
{
    [SerializeField] public LevelModel CurrentLevelModel;

    static LevelModel BasicLevel = new LevelModel
    {
        LevelNumber = 0,
        IsBossLevel = false,

        LogMinRotationSpeed = -500f,
        LogMaxRotationSpeed = 500f,
        SmoothLogRotation = 0.7f,

        MinTimeRotation = 1f,
        MaxTimeRotation = 3f,
        IsLogRotating = true,

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
        int MaxLevel = PlayerPrefs.GetInt("MaxLevel", 0);

        if(CurrentLevel > MaxLevel)
        {
            PlayerPrefs.SetInt("MaxLevel", CurrentLevel);
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
    void CheckAndSetMaxLvl ()
    {
        int CurrentLevel = PlayerPrefs.GetInt("LevelNumber");
    }
    public void NextLevel()
    {
        LevelModel nextLevel = CurrentLevelModel;

        nextLevel.LevelNumber++;
        nextLevel.IsLogRotating = true;
        nextLevel.IsBossLevel = false;

        PlayerPrefs.SetInt("LevelNumber", nextLevel.LevelNumber);

        if(nextLevel.LevelNumber % 4 == 0)
        {
            nextLevel.IsBossLevel = true;
            nextLevel.LogShakingSpeed = 5f;
        }

        if (nextLevel.LogMaxRotationSpeed > -1500)
            nextLevel.LogMaxRotationSpeed *= 1.01f;

        if (nextLevel.LogMinRotationSpeed < 1500)
            nextLevel.LogMinRotationSpeed*= 1.01f;

        if (nextLevel.MaxTimeRotation > 0.5f)
            nextLevel.MaxTimeRotation -= 0.01f;

        if (nextLevel.MinTimeRotation > 1f)
            nextLevel.MinTimeRotation -= 0.01f;

        CurrentLevelModel = nextLevel;
    }
}

[Serializable]
public struct LevelModel
{
    public int LevelNumber;
    public bool IsBossLevel;

    public float LogMinRotationSpeed;
    public float LogMaxRotationSpeed;
    public float SmoothLogRotation;

    public float MinTimeRotation;
    public float MaxTimeRotation;
    public bool IsLogRotating;

    public float LogShakingSpeed;
    [Range(2,6)]
    public int KnifeCount;
    public float SpawnKnifeDelay;
}
