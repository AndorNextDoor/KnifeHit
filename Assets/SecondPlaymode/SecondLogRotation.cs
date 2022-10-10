using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SecondLogRotation : MonoBehaviour
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
    SecondLevelManager Levelmanager;
    private Vector3 BossPos;
    public Quaternion b;
    public bool torotate;

    public Transform transformRot;
    public int[] randomDegree;

    //
    public float turnSpeed;

  public  Vector3 dir;
    //
    public void Awake()
    {

    }
    public void Start()
    {
        LogPos = transform.position;
        LogRot = transform.rotation;
        Levelmanager = FindObjectOfType<SecondLevelManager>();
        StartCoroutine(RotateLog());

    }
    private void Update()
    {
 
        
        
    }

    IEnumerator RotateLog()
    {

        while (true)
        {

            
            int[] angles = new int[] { -90, 90 };
            int angle = angles[Random.Range(0, angles.Length)];
            float TimeToRotate = 1f;
            float TimeBetween = Random.Range(0.5f, 2f);
            var FromAngle = transform.rotation;
            var toAngle = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, angle));
            for (float t = 0f; t < 1; t += Time.deltaTime / TimeToRotate)
            {
                
                transform.rotation = Quaternion.Slerp(FromAngle, toAngle, t);

                yield return null;
                
            }
            yield return new WaitForSeconds(TimeBetween);
        }
    }
}

   



