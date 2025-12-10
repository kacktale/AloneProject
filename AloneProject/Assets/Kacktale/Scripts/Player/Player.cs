using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D rb;
    public PlayerTurnUI playerTurnUI;
    public bool isInvincible = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Speed = 1;
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            Speed = 2;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        //transform.position += new Vector3(h, v, 0) * Time.deltaTime * Speed;
        rb.velocity = new Vector2(h, v) * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("DontDestroyBullet"))
        {
            if (!isInvincible)
            {
                playerTurnUI.RandomPlayerDamage();
                TurnOnInvincible();
            }
            if(collision.gameObject.CompareTag("Bullet")) Destroy(collision.gameObject);
        }
    }

    private void TurnOnInvincible()
    {
        isInvincible = true;
        Invoke("TurnOffInvincible",0.5f);
    }

    private void TurnOffInvincible()
    {
        isInvincible = false;
    }
}
