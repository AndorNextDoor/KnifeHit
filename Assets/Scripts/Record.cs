using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Record : MonoBehaviour
{
    public TextMeshProUGUI recordnum;

    void Awake()
    {
        recordnum.text = PlayerPrefs.GetInt("MaxLevel", 0).ToString();
    }


}
