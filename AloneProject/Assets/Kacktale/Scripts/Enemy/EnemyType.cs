using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy",menuName ="Enemy/BossType",order =1)]
public class EnemyType : ScriptableObject
{
    public string Name;

    public int Hp;
    public int MaxHp;

    public int Def;

    public int ATK;
}
