using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField]
    private GameObject DestroyedVersion;
   
    public void DestroyLog()
    {
        Destroy(Instantiate(DestroyedVersion, transform.position, transform.rotation), 0.5f);
        Destroy(gameObject);
    }
}
