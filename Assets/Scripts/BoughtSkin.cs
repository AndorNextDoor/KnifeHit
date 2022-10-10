using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoughtSkin : MonoBehaviour
{
    public GameObject skinMenu;
    void Awake()
    {
        if (PlayerPrefs.GetInt("Bought", 1) == 1)
        {
            skinMenu.gameObject.SetActive(true);
            PlayerPrefs.SetInt("Bought", 0);
        }
        else
            return;
    }

}
