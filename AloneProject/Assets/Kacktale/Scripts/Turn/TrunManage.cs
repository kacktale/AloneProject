using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunManage : MonoBehaviour
{
    public bool IsPlayerTurn = true;
    public bool GameStart = true;
    public GameObject Player;
    public PlayerTurnUI PlayerTurn;
    public EnemyCreateHit Enemysc;
    public GameObject FadeZone;
    // Update is called once per frame
    void Update()
    {
        if(IsPlayerTurn) Player.SetActive(false);
        else Player.SetActive(true);
    }

    public void StartEnemyTurn()
    {
        FadeZone.SetActive(true);
        IsPlayerTurn = false;
        Enemysc.Maketurn();
    }
    public void StartPlayerTurn()
    {
        FadeZone.SetActive(false);
        IsPlayerTurn = true;
        PlayerTurn.StartAct();
    }
}
