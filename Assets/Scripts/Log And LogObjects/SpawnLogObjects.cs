using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLogObjects : MonoBehaviour
{
    [SerializeField]
    public GameObject[] LogObjects;
    [SerializeField]
    GameController gameController;
    [SerializeField]
    LevelManager levelManager;
    [SerializeField]
    GameObject KnifeSpawn;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        gameController = FindObjectOfType<GameController>();
        SpawnLogObject();   

    }

    // Update is called once per frame
    void SpawnLogObject ()
    {
        int[] Angles = new int[] { 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330, 360 };
        int Knives = levelManager.Knives;
        int Asses = levelManager.Asses;
        int EveryObject = Knives + Asses;
        Vector3 KnifePos;
        string usedangle = " ";
        int safetynet = 0;
        
        for (int i = 0; i < EveryObject; i++)
        {
            GameObject knife = null;


            if (i < Knives)
            {
                knife = Instantiate(KnifeSpawn, FindObjectOfType<LogRotation>().transform);
                knife.GetComponent<Rigidbody2D>().isKinematic = true;
                Destroy(knife.GetComponent<Knife>(), 0f);
            }
            else
            {

                GameObject randomObject;
                
                float randomValue = Random.Range(0f, 1f);

                
                if (randomValue < 0.95f)
                {
                    randomObject = LogObjects[0];
                }
                else
                {
                    randomObject = LogObjects[1];
                }

                knife = Instantiate(randomObject);
            }
           

        

            
            int Angle = Random.Range(0, Angles.Length);
            while(usedangle.Contains(" " + Angle) && safetynet <20)
            {
                Angle = Random.Range(0, Angles.Length);
                safetynet++;
            }
            usedangle = usedangle + " " + Angle;

                
            KnifePos.x = Mathf.Cos(Angles[Angle] * Mathf.PI / 180) * 1.1f;
            KnifePos.y = Mathf.Sin(Angles[Angle] * Mathf.PI / 180) * 1.1f;
            KnifePos.z = 0;
            KnifePos += transform.position;
            knife.transform.position = KnifePos;
            knife.transform.eulerAngles = new Vector3(0, 0, 90 + Angles[Angle]);
            knife.transform.parent = transform;

        }
    }
}
