using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameUI))]
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    [SerializeField]
    private GameObject LogPrefab;
    [SerializeField]
    public LogRotation LogController;
    [SerializeField]
    private int KnifeCount;
    [Header("Knife Spawning")]
    [SerializeField]
    private Vector2 KnifeSpawnPosition;
    [SerializeField]
    public GameObject knifeObject;
    [SerializeField]
    private Destructible Destr;
    [SerializeField]
    private GameObject Bomb;
    [SerializeField]
    public Transform kniferot;
    [SerializeField]
    SkinController Skin;


    [SerializeField]
    LevelManager levelManager;


    public GameUI GameUI { get; private set; }

    private void Awake()
    {
        LogController = FindObjectOfType<LogRotation>();
        levelManager = FindObjectOfType<LevelManager>();
        levelManager.SetLevelZero();
        KnifeCount = Random.Range(1, 1);
        Instance = this;
        GameUI = GetComponent<GameUI>();
    }

    private void Start()
    {
        GameUI.SetInitialDisplayKnifeCount(KnifeCount);
        SpawnKnife();
    }

    public void OnSuccessufulKnifeHit()
    {
        if (KnifeCount > 0)
        {
            SpawnKnife();
        }
        else
        {
            StartCoroutine(NextLevel(true));
        }
    }

    private void SpawnKnife()
    {
        KnifeCount--;
        Instantiate(knifeObject, KnifeSpawnPosition, Quaternion.identity);
    }
    public void WinCheck(bool win)
    {
        StartCoroutine(NextLevel(win));
    }
    public void ClearKnives()
    {
      //  foreach
    }



 
    public void StartFromZero()
    {
        LogController.ResetLogPos();
        levelManager.SetLevelZero();
        SpawnKnife();

    }
    public IEnumerator NextLevel(bool win)
    {
        if (win)
        {
            Destr.DestroyLog();
            Destroy(Instantiate(Bomb, Bomb.transform.position, Quaternion.identity), 0.3f);
            levelManager.CurrentLevelModel.IsBossLevel = false;
            levelManager.CurrentLevelModel.IsLogRotating = false;
           
            win = false;
            yield return new WaitForSeconds(0.5f);
            Instantiate(LogPrefab);
            Skin = FindObjectOfType<SkinController>();
            levelManager.NextLevel();
            Skin.SetLoverSkin();
            KnifeCount = Random.Range(1, 1);
            GameUI.SetInitialDisplayKnifeCount(KnifeCount);
            LogController = FindObjectOfType<LogRotation>();
            levelManager = FindObjectOfType<LevelManager>();
            Destr = FindObjectOfType<Destructible>();
            GameUI.UpdateUiLevelValue();
          //  ObjectsController = FindObjectOfType<LogObjectsController>();
            //ObjectsController.SpawnLogObjects();
            SpawnKnife();
           

            


        }
        else
        {
            Debug.Log(PlayerPrefs.GetInt("maxLevel"));
            GameUI.ShowRestartButton();
        }
    }
    public void RestartTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
