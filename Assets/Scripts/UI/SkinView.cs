using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinView : MonoBehaviour
{
    public GameObject SkinSelect;

    public void ChooseSkinToView()
    {
        SkinSelect.GetComponent<Image>().sprite = gameObject.transform.parent.GetComponent<Image>().sprite;
    }

}
