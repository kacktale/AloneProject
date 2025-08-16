using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public bool Rotateturn = false;
    void FixedUpdate()
    {
        if(!Rotateturn) transform.position += transform.up * Time.deltaTime * speed;
        if(transform.position.x <-8.5f) Destroy(gameObject);
    }

    public void RotateSetup()
    {
        if(!Rotateturn) return;
        transform.position -= transform.up * 3;
    }
    public void RotateMove()
    {
        Rotateturn = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DestroyZone"))
        {
            Destroy(gameObject);
        }
    }
}
