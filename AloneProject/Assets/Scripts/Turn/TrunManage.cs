using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunManage : MonoBehaviour
{
    public bool IsPlayerTurn = true;
    public bool GameStart = true;
    public GameObject Player;
    // Update is called once per frame
    void Update()
    {
        if(IsPlayerTurn) Player.SetActive(false);
        else Player.SetActive(true);
    }

    public virtual void AttackEnemy(float Damage)
    {

    }
}
