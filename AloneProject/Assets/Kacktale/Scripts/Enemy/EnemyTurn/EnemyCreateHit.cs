using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyCreateHit : MonoBehaviour
{
    public int currentPhaze;
    public TrunManage turnManage;

    public GameObject Bullet;
    public GameObject Cannon;
    public GameObject Track;
    public Transform EnemyPos;
    public Transform PlayerPos;

    public bool EnemyTurn;

    private List<Bullet> bullets = new List<Bullet>();
    public void Maketurn()
    {
        EnemyTurn = true;
        CheckPhaze(currentPhaze);
        currentPhaze++;
    }

    void CheckPhaze(int turn)
    {
        switch (turn)
        {
            case 0: StartCoroutine(StartFirstTurn()); break;
            case 1: StartCoroutine(StartSecondTurn()); break;
            case 2: StartCoroutine(StartThrdTurn()); break;
            case 3: StartCoroutine(StartForthTurn()); break;
            default: StartRandomPhaze(); break;
        }
    }

    void StartRandomPhaze()
    {
        int randomTurn = Random.Range(0,4);
        CheckPhaze(randomTurn);
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
        yield return new WaitForSeconds(1f);
        EndTurn();
    }

    IEnumerator StartThrdTurn()
    {
        for (int i = 0; i < 9; i++)
        {
            float leftX = Random.Range(3f,6f);
            float rightX = Random.Range(-3f, -6f);
            bool isleft = Random.value < 0.5f;
            Vector3 Pos = new Vector3(0,6,0);
            if(isleft) Pos.x = leftX;
            else Pos.x = rightX;

            EnemyCannon cannon = Instantiate(Cannon,Pos, Quaternion.identity).GetComponent<EnemyCannon>();
            cannon.IsLeft = isleft;
            cannon.PlayerPos = PlayerPos;      
            yield return new WaitForSeconds(0.6f);
        }
        yield return new WaitForSeconds(2.6f);
        EndTurn();
    }

    IEnumerator StartForthTurn()
    {
        GameObject chainSaw = Instantiate(Track,Vector3.up,Quaternion.identity);
        yield return new WaitForSeconds(1f);
        float t = 0;
        while (t < 15f)
        {
            chainSaw.transform.Rotate(Vector3.forward * 0.5f);
            t += Time.deltaTime;
            yield return null;
        }
        Destroy(chainSaw);
        yield return new WaitForSeconds(1.2f);
        EndTurn();
    }

    void EndTurn()
    {
        turnManage.StartPlayerTurn();
        EnemyTurn = false;
    }
}
