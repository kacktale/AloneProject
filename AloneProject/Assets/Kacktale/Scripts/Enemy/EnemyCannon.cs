using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannon : MonoBehaviour
{
    public GameObject bullet;
    public bool IsLeft;
    public Transform PlayerPos;

    private float disappearVec;
    // Start is called before the first frame update
    void Start()
    {
        disappearVec = Random.Range(2.45f,-0.41f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanDisappear()) transform.position += Vector3.down * Time.deltaTime * 5;
        else
        {
            CannonBullet bullets = Instantiate(bullet,transform.position,Quaternion.identity).GetComponent<CannonBullet>();
            bullets.isLeft = IsLeft;
            bullets.playerPos = PlayerPos;
            Destroy(gameObject);
        }
    }

    bool CanDisappear()
    {
        return transform.position.y <= disappearVec;
    }
}
