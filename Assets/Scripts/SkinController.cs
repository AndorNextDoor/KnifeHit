using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinController : MonoBehaviour
{
    [SerializeField]
    public Sprite[] BossSprite;
    [SerializeField]
    public Sprite[] BasicSprite;
    public string[] SoundToPlay;
    public string[] BossSoundToPlay;
    public string Sound;

    public int BossNum = 0; 
    LogRotation Log;
    LevelManager levelManager;
    // Start is called before the first frame update
    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }


   public void SetLoverSkin()
    {
        
        Log = FindObjectOfType<LogRotation>();
        if (levelManager.CurrentLevelModel.IsBossLevel)
        {
            Log.GetComponent<SpriteRenderer>().sprite = BossSprite[BossNum];
            Sound = BossSoundToPlay[BossNum];
        }
        else
        {
            int random;
            random = Random.Range(0, 2);
            Log.GetComponent<SpriteRenderer>().sprite = BasicSprite[random];
            Sound = SoundToPlay[random];
        }
        
    }
    public void SetBGSkin()
    {

    }
}
