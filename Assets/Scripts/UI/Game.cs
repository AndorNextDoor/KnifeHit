using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public int Coins;
    public int GoldCoins;

    public static Game Instance;
   
    public void Start()
    {
        Coins = PlayerPrefs.GetInt("ApplesCount");
    }
    public void UseCoins (int Amount)
    {
        Coins = PlayerPrefs.GetInt("ApplesCount");
        Coins -= Amount;
        PlayerPrefs.SetInt("ApplesCount", Coins);
    }
    public bool HasEnoughCoins (int Amount)
    {
        Coins = PlayerPrefs.GetInt("ApplesCount");
        return (Coins >= Amount);
    }
    public void UseGoldCoins(int Amount)
    {
        GoldCoins = PlayerPrefs.GetInt("GoldApplesCount");
        GoldCoins -= Amount;
        PlayerPrefs.SetInt("GoldApplesCount", GoldCoins);
    }
    public bool HasEnoughGoldCoins(int Amount)
    {
        GoldCoins = PlayerPrefs.GetInt("GoldApplesCount");
        return (GoldCoins >= Amount);
    }
}
