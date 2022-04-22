using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogObjectsController : MonoBehaviour
{
    [SerializeField]
    public GameObject[] LogObjects;
    [SerializeField]
    public GameObject[] spawnedLogObjects;
    [SerializeField]
    public GameController gameController;

    public void Start()
    {
        SpawnLogObjects();
    }

    public void SpawnLogObjects()
    {
        int MaxKnives = Random.Range(2, 4);
        int MaxLogObjects = Random.Range(1, 6);
        int totalMaxObjects = MaxKnives + MaxLogObjects;

        List<int> angles = GetRandomAnglesForSpawn(totalMaxObjects);

        spawnedLogObjects = new GameObject[angles.Count];

        float rad = 0;

        for (int i = 0; i < angles.Count; i++)
        {

            Vector3 knifePos;
            GameObject obj = null;


            if (i <= MaxKnives - 1)
            {
                obj = Instantiate(gameController.knifeObject);
                spawnedLogObjects[i] = obj;
                spawnedLogObjects[i].GetComponent<Rigidbody2D>().isKinematic = true;
                Destroy(spawnedLogObjects[i].GetComponent<Knife>(), 0f);

                rad = spawnedLogObjects[i].GetComponent<Knife>().SpawnKnifeY;
            }
            else
            {
                GameObject randomObject = LogObjects[Random.Range(0, LogObjects.Length)];

                float randomValue = Random.Range(0f, 1f);
                float currentObjcetsSpawnProbabilityLow = randomObject.GetComponent<LogObjectsContainer>().LogData.SpawnProbabilityLow;
                float currentObjcetsSpawnProbabilityHigh = randomObject.GetComponent<LogObjectsContainer>().LogData.SpawnProbabilityHigh;
                if (randomValue < currentObjcetsSpawnProbabilityLow && randomValue > currentObjcetsSpawnProbabilityHigh)
                {
                    return;
                }

                obj = Instantiate(randomObject);
                spawnedLogObjects[i] = obj;
                rad = randomObject.GetComponent<LogObjectsContainer>().LogData.LogObjectsSpawnY;
            }

            knifePos.x = Mathf.Cos(angles[i] * Mathf.PI / 180) * rad;
            knifePos.y = Mathf.Sin(angles[i] * Mathf.PI / 180) * rad;
            knifePos.z = 0;
            knifePos += transform.position;

            spawnedLogObjects[i].transform.position = knifePos;
            spawnedLogObjects[i].transform.eulerAngles = new Vector3(0, 0, 90 + angles[i]);
            spawnedLogObjects[i].transform.parent = transform;
        }
    }

    List<int> GetRandomAnglesForSpawn(int count)
    {
        List<int> spawnAngles = new List<int>();

        for (int i = 0; i < count; i++)
        {
            int angle;

            for (; ; )
            {
                angle = Random.Range(0, 360);

                if (spawnAngles.Count == 0)
                {
                    break;
                }

                if (spawnAngles.Contains(angle))
                {
                    continue;
                }

                break;
            }

            spawnAngles.Add(angle);
        }

        return spawnAngles;
    }
}

