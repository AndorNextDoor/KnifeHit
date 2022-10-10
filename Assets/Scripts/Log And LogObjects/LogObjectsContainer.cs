using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogObjectsContainer : MonoBehaviour
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
        UpdateApplesCount();
        gameui.UpdateUiApplesCount();
    }
    public void UpdateApplesCount()
    {
        int AC = PlayerPrefs.GetInt("ApplesCount");
        AC++;
        PlayerPrefs.SetInt("ApplesCount", AC);
    }
}
