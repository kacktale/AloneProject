using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerType
{
    public string Name;

    public float Hp;
    public float MaxHp;

    public float Def;

    public float ATK;
}
[System.Serializable]
public class PlayerAct
{
    public int ActNum; 
}

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObject/PlayerType", order = 1)]
public class Players : ScriptableObject
{
    public PlayerType[] PlayerTypes;
    public PlayerAct[] PlayerActs;
}
