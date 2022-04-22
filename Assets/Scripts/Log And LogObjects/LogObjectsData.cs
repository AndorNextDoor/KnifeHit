using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Objects Data", menuName = "Objects Data")]
public class LogObjectsData : ScriptableObject
{
    [SerializeField] public float SpawnProbabilityLow;
    [SerializeField] public float SpawnProbabilityHigh;
    [SerializeField] public Image Img;
    [SerializeField] public string Name;
    [SerializeField] public float LogObjectsSpawnY;
}

