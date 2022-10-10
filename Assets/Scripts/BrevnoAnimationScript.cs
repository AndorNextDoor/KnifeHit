using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrevnoAnimationScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Animation>().Play();
    }



}
