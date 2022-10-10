using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panelicon : MonoBehaviour
{
    public Knife knife;
    void Start()
    {
        this.GetComponent<Image>().sprite = knife.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
