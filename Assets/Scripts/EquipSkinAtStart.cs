using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSkinAtStart : MonoBehaviour
{
    public GameObject Skinmenu;
    public static EquipSkinAtStart instance;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
        void Start()
    {
        StartCoroutine(StartSkinMenu());
    }
    IEnumerator StartSkinMenu()
    {
        Skinmenu.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.001f);
        Skinmenu.gameObject.SetActive(false);
    }
}
