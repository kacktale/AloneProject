using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerType
{
    public string Name;

    public int Hp;
    public int MaxHp;

    public int Def;

    public int ATK;
}
public class Players : MonoBehaviour
{
    public PlayerType[] PlayerTypes;
}
