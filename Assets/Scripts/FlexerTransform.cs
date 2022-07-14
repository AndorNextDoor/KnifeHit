using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlexerTransform : MonoBehaviour
{
    private Transform target;
    // Start is called before the first frame update
    void Awake()
    {
        target = FindObjectOfType<LogRotation>().transform;    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = target.position; 
    }
}
