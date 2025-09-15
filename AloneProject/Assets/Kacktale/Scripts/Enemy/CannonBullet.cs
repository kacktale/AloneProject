using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    float rotateSpeed;
    float moveSpeed;
    public bool isLeft;
    float rotateAngle;

    public Transform playerPos;
    public Transform rotateObj;
    Vector3 TargetPos;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(playerPos);
        TargetPos = playerPos.position;
        rotateSpeed = Random.Range(10f, 40f);
        moveSpeed = Random.Range(3f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;

        //if (isLeft) transform.position += Vector3.left * Time.deltaTime * moveSpeed;
        //else transform.position += Vector3.right * Time.deltaTime * moveSpeed;

        rotateAngle += 1 * rotateSpeed * Time.deltaTime;
        rotateObj.rotation = Quaternion.Euler(0, 0, rotateAngle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DestroyZone"))
        {
            Destroy(gameObject);
        }
    }
}
