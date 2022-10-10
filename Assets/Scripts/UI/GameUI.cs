using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI textApplesCount;
    [SerializeField] public TextMeshProUGUI textLevelNumber;
    [SerializeField] public TextMeshProUGUI textLevelRecord;
    [SerializeField] public TextMeshProUGUI textGoldApplesCount;


    [SerializeField]
    private GameObject gameobject;

    [Header("Knife Count Display")]
    [SerializeField]
    private GameObject KnifePanel;
    [SerializeField]
    private GameObject KnifeIcon;
    [SerializeField]
    private Color UsedKnifeColor;
    

    public void Start()
    {
        UpdateUiApplesCount();
        UpdateUiLevelValue();
        UpdateLevelRecord();
        UpdateUiGoldApplesCount();
    }
    public void ShowRestartButton ()
    {
        gameobject.SetActive(true);
        Time.timeScale = 0.8f;
    }
    public void SetInitialDisplayKnifeCount(int count)
    {
        for (int i = 0; i < count; i++)
            Instantiate(KnifeIcon, KnifePanel.transform);
        KnifeIconIndexToChange = 0;
    }
    private int KnifeIconIndexToChange;
    public void DecrementDispayedKnifeCount()
    {
        KnifePanel.transform.GetChild(KnifeIconIndexToChange++)
            .GetComponent<Image>().color = UsedKnifeColor;
    }

    public void DestroyKnifeCount()
    {
        foreach (Transform child in KnifePanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        return;
    }
    
    public void UpdateUiApplesCount()
    {
        textApplesCount.text = "" + PlayerPrefs.GetInt("ApplesCount", 0);
    }
    public void UpdateUiGoldApplesCount()
    {
        textGoldApplesCount.text = "" + PlayerPrefs.GetInt("GoldApplesCount", 0);
    }
    public void UpdateUiLevelValue()
    {
        textLevelNumber.text = "LEVEL" + PlayerPrefs.GetInt("LevelNumber", 0);
        PlayerPrefs.Save();
    }
    public void UpdateLevelRecord()
    {
        textLevelRecord.text = "" + PlayerPrefs.GetInt("MaxLevel", 0);
    }
}
