using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] public Text textApplesCount;
    [SerializeField] public Text textLevelNumber;

    [SerializeField]
    private GameObject RestartButton;

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
    }
    public void ShowRestartButton ()
    {
        RestartButton.SetActive(true);
    }
    public void SetInitialDisplayKnifeCount(int count)
    {
        for (int i = 0; i < count; i++)
            Instantiate(KnifeIcon, KnifePanel.transform);
    }

    private int KnifeIconIndexToChange = 0;
    
    public void DecrementDispayedKnifeCount()
    {
        KnifePanel.transform.GetChild(KnifeIconIndexToChange++)
            .GetComponent<Image>().color = UsedKnifeColor;
    }
    public void UpdateUiApplesCount()
    {
        textApplesCount.text = "Apples: " + PlayerPrefs.GetInt("ApplesCount", 0);
    }
    public void UpdateUiLevelValue()
    {
        textLevelNumber.text = "Level: " + PlayerPrefs.GetInt("LevelNumber", 0);
    }
}
