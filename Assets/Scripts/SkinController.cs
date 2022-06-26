using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinController : MonoBehaviour
{
    [SerializeField]
    public Sprite[] BossSprite;
    [SerializeField]
    public Sprite[] BasicSprite;
    public int BossNum = 0; 
    LogRotation Log;
    LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
   public void SetLoverSkin()
    {
        
        Log = FindObjectOfType<LogRotation>();
        if (levelManager.CurrentLevelModel.IsBossLevel)
        {
            Log.GetComponent<SpriteRenderer>().sprite = BossSprite[BossNum];
        }
        else
        {
            Log.GetComponent<SpriteRenderer>().sprite = BasicSprite[Random.Range(0,2)];
        }
        
    }
}
