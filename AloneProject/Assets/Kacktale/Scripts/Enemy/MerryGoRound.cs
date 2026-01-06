using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MerryGoRound : MonoBehaviour
{
    public Vector3 rightEndPos;
    public Vector3 leftEndPos;
    public bool IsBackSide;
    public bool GoingUp;

    public float speed;

    public SpriteRenderer[] sprites;
    public CircleCollider2D[] colliders;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            if (IsBackSide)
            {
                sprites[i].color = Color.gray;
                sprites[i].sortingOrder = 9;
                colliders[i].enabled = false;
            }
            else
            {
                sprites[i].color = Color.white;
                sprites[i].sortingOrder = 40;
                colliders[i].enabled = true;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = Vector3.zero;
        if (!IsBackSide)
        {
            if (GoingUp) pos.y = 0.2f;
            else pos.y = -0.2f;
            pos.x = 1;
        }
        else
        {
            if (GoingUp) pos.y = -0.2f;
            else pos.y = 0.2f;
            pos.x = -1;
        }

        transform.position += pos * Time.deltaTime * speed;

        if (Mathf.Abs(transform.position.x - rightEndPos.x) <= 0.4f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            IsBackSide = true;
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].color = Color.gray;
                sprites[i].sortingOrder = 9;
                colliders[i].enabled = false;
            }
        }
        else if (Mathf.Abs(transform.position.x - leftEndPos.x) <= 0.4f)
        {
            transform.localScale = Vector3.one;
            IsBackSide = false;
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].color = Color.white;
                sprites[i].sortingOrder = 40;
                colliders[i].enabled = true;
            }
        }
    }
}
