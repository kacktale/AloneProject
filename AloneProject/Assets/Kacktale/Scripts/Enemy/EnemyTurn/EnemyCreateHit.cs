using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyCreateHit : MonoBehaviour
{
    public int currentPhaze;
    public TrunManage turnManage;

    public GameObject Bullet;
    public Transform EnemyPos;

    public bool EnemyTurn;

    private List<Bullet> bullets = new List<Bullet>();
    public void Maketurn()
    {
        EnemyTurn = true;
        CheckPhaze();
        currentPhaze++;
    }

    void CheckPhaze()
    {
        switch (currentPhaze)
        {
            case 0: StartCoroutine(StartFirstTurn()); break;
            case 1: StartCoroutine(StartSecondTurn()); break;
        }
    }

    IEnumerator StartFirstTurn()
    {
        int bulletMax = 8;
        int turnMax = 4;
        for (int t = 0; t < turnMax; t++)
        {
            for (int i = 0; i < bulletMax; i++)
            {
                int j = i;
                j -= bulletMax / 2;
                int turn = t;
                turn = t % 2 == 0 ? -1 : 1;

                float angle = (108 / bulletMax) * j + 90f + turn * 15;
                Instantiate(Bullet, EnemyPos.position, Quaternion.Euler(0, 0, angle));
            }
            yield return new WaitForSeconds(2);
        }
        yield return new WaitForSeconds(1);
        EndTurn();
    }
    IEnumerator StartSecondTurn()
    {
        int bulletMax = 8;
        int turnMax = 4;
        for (int t = 0; t < turnMax; t++)
        {
            for (int i = 0; i < bulletMax; i++)
            {
                int j = i;
                j -= bulletMax / 2;

                float angle = (360 / bulletMax) * j + 180f;
                GameObject rotateBullet = Instantiate(Bullet, transform.position, Quaternion.Euler(0, 0, angle));
                rotateBullet.transform.localScale = Vector3.one;
                Bullet bullet = rotateBullet.GetComponent<Bullet>();
                bullet.Rotateturn = true;
                bullet.RotateSetup();
                bullets.Add(bullet);
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.1f);

            int turn = t;
            turn = t % 2 == 0 ? -1 : 1;
            int reverse = 0;
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                yield return new WaitForSeconds(0.3f);
                if (turn == 1)
                    bullets[i].RotateMove();
                else bullets[reverse].RotateMove();
                reverse++;
            }
            bullets.Clear();
        }
        EndTurn();
    }

    void EndTurn()
    {
        turnManage.StartPlayerTurn();
        EnemyTurn = false;
    }
}
