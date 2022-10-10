using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField]
    public Flickering flickering;
    [SerializeField]
    public GameUI Gameui;
    [SerializeField]
    private Vector2 trowForce;

    private bool IsActive = true;

    private Rigidbody2D rb;
    private BoxCollider2D Col;
    private AudioManager audioManager;
    private float timer;
    private float time = 0.12f;
    

    private void Awake()
    {
        flickering = FindObjectOfType<Flickering>();
        audioManager = FindObjectOfType<AudioManager>();
        Gameui = FindObjectOfType<GameUI>();
        rb = GetComponent<Rigidbody2D>();
        Col = GetComponent<BoxCollider2D>();

    }
    private void Start()
    {
        gameObject.GetComponent<Animation>().Play();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= time)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.position.y < Screen.height / 2)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        rb.AddForce(trowForce, ForceMode2D.Force);
                        rb.gravityScale = -15;
                        GameController.Instance.GameUI.DecrementDispayedKnifeCount();
                    }
                }
            }
        }
#if UNITY_EDITOR 
        if (timer >= time)
        {
            if (Input.GetMouseButtonDown(0) && IsActive)
            {
                rb.AddForce(trowForce, ForceMode2D.Force);
                rb.gravityScale = -15;
                GameController.Instance.GameUI.DecrementDispayedKnifeCount();
            }
        }
#endif
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsActive) 
            return;
        IsActive = false;
        if (collision.collider.tag == "Knife")
        {
            audioManager.Play("Augh");
            rb.gravityScale = 5f;
            rb.velocity = new Vector2(rb.velocity.x, -10f);
            GameController.Instance.WinCheck(false);
        }
        if (collision.collider.tag == "Log")
        {
            audioManager.Play("Slap");
            GetComponent<ParticleSystem>().Play();
            flickering.Flick();
            rb.velocity = new Vector2(0, 0);
            rb.bodyType = RigidbodyType2D.Kinematic;
            this.transform.SetParent(collision.collider.transform);
            this.transform.localScale = new Vector3(0.4f, 0.4f);
            GameController.Instance.OnSuccessufulKnifeHit();
            Destroy(this.GetComponent<Knife>());
        }
    }
}
