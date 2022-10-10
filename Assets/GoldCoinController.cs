using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoinController : MonoBehaviour
{
    public ParticleSystem part;
    public GameUI gameui;
    // Start is called before the first frame update

    public void Start()
    {
        gameui = FindObjectOfType<GameUI>().GetComponent<GameUI>();
        Collider2D coll = transform.GetComponent<Collider2D>();
        coll.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<Knife>())
        {
            return;
        }

        Instantiate(part, transform.position, transform.rotation);
        Destroy(gameObject);
        UpdateGoldApplesCount();
        gameui.UpdateUiGoldApplesCount();
    }
    public void UpdateGoldApplesCount()
    {
        int AC = PlayerPrefs.GetInt("GoldApplesCount");
        AC++;
        PlayerPrefs.SetInt("GoldApplesCount", AC);
    }
}
