using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerfightUI : MonoBehaviour
{
    public bool PlayerOneReady = false;
    public bool PlayerTwoReady = false;
    public bool PlayerThreeReady = false;

    public GameObject[] PlayerAttackUI;
    public GameObject HitNote;

    public virtual void MakeFightList()
    {
        int AttackCount = 3;
        if (!PlayerOneReady && !PlayerTwoReady && !PlayerThreeReady) return;
        if (!PlayerOneReady) PlayerAttackUI[0].SetActive(false); AttackCount--;
        if (!PlayerTwoReady) PlayerAttackUI[1].SetActive(false); AttackCount--;
        if (!PlayerThreeReady) PlayerAttackUI[2].SetActive(false); AttackCount--;
        MakeHitNote(AttackCount);
    }

    void MakeHitNote(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int DelayTime = Random.Range(0, 2);
            GameObject note = Instantiate(HitNote,transform.position,Quaternion.identity);
            note.transform.position -= new Vector3((float)DelayTime,0) * 3;
        }
    }
}
