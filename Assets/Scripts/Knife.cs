using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField]
    public GameUI Gameui;
    [SerializeField]
    private Vector2 trowForce;
    [SerializeField]
    private float KnifeDelay;

    private bool IsActive = true;

    private Rigidbody2D rb;
    private BoxCollider2D Col;
    [SerializeField]
    public float SpawnKnifeY = 1f;

    private void Awake()
    {
        Gameui = FindObjectOfType<GameUI>();
        rb = GetComponent<Rigidbody2D>();
        Col = GetComponent<BoxCollider2D>();

    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && IsActive)
        {
            rb.AddForce(trowForce, ForceMode2D.Impulse);
            rb.gravityScale = 1;
            GameController.Instance.GameUI.DecrementDispayedKnifeCount();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsActive)
            return;
        IsActive = false;

        if(collision.collider.tag == "Log")
        {
            GetComponent<ParticleSystem>().Play();

            rb.velocity = new Vector2(0, 0);
            rb.bodyType = RigidbodyType2D.Kinematic;
            this.transform.SetParent(collision.collider.transform);
            GameController.Instance.OnSuccessufulKnifeHit();
        }
        else if(collision.collider.tag == "Knife")
        {
            rb.velocity = new Vector2(rb.velocity.x, -10f);
            GameController.Instance.WinCheck(false);        
        }
    }
}
