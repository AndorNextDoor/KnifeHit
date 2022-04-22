using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LogRotation : MonoBehaviour
{
    [SerializeField]
    Vector3 LogPos;
    [SerializeField]
    Quaternion LogRot;
    [SerializeField]
    private float RotationSpeed;
    [SerializeField]
    private float TimeToChange; // Рандомное время до изменения вращения
    [SerializeField]
    private float LogRotationSpeedChanger; //Рандомная скорость вращения

    LevelManager Levelmanager;

    public void Start()
    {
        LogPos = transform.position;
        LogRot = transform.rotation;
        Levelmanager = FindObjectOfType<LevelManager>();
        StartCoroutine(ChangeLogStats());
    }
    public void Update()
    {
        LogRotationElement();
    }

    void LogRotationElement() //Вращаем бревнышко
    {
        if (Levelmanager.CurrentLevelModel.IsLogRotating)
        {
            RotationSpeed = Mathf.Lerp(RotationSpeed, LogRotationSpeedChanger, Levelmanager.CurrentLevelModel.SmoothLogRotation * Time.deltaTime);
            Levelmanager.CurrentLevelModel.SmoothLogRotation = 0.6f;
        }
        else
        {
            RotationSpeed = Mathf.Lerp(RotationSpeed, 0, Levelmanager.CurrentLevelModel.SmoothLogRotation * Time.deltaTime);
            Levelmanager.CurrentLevelModel.SmoothLogRotation = 10f;
        }

        transform.Rotate(new Vector3(0, 0, RotationSpeed * Time.deltaTime));
    }

    IEnumerator ChangeLogStats() //Меняем статы
    {
        while (true)
        {
            TimeToChange = Random.Range(Levelmanager.CurrentLevelModel.MinTimeRotation, Levelmanager.CurrentLevelModel.MaxTimeRotation);
            LogRotationSpeedChanger = Random.Range(Levelmanager.CurrentLevelModel.LogMinRotationSpeed, Levelmanager.CurrentLevelModel.LogMaxRotationSpeed);
            yield return new WaitForSeconds(TimeToChange);
        }
    }

    public void ResetLogPos()
    {
        transform.position = LogPos;
        transform.rotation = LogRot;
    }

    void LogShaking()
    {
        float distance = Mathf.Sin(Time.timeSinceLevelLoad * Levelmanager.CurrentLevelModel.LogShakingSpeed);
        transform.position = new Vector3(0, distance + 2f, 0);
    }
}




