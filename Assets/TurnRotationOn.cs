using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRotationOn : MonoBehaviour
{

    void Start()
    {
        this.GetComponent<Animation>().Play("BrevnoStart");
        StartCoroutine(TurnOn());
    }
    IEnumerator TurnOn()
    {
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<LogRotation>().enabled = true;
    }
}
