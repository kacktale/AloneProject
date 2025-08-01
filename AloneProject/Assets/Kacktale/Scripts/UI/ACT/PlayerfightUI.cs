using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerfightUI : MonoBehaviour
{
    public bool PlayerOneReady = false;
    public bool PlayerTwoReady = false;
    public bool PlayerThreeReady = false;

    public GameObject[] PlayerAttackUI;
    public RectTransform[] PlayerRectTransform;
    public GameObject HitNote;
    public List<NoteManager> Notes;
    public TrunManage trunManage;
    public virtual void MakeFightList()
    {
        if (!PlayerOneReady && !PlayerTwoReady && !PlayerThreeReady) return;
        if (!PlayerOneReady) PlayerAttackUI[0].SetActive(false);
        if (!PlayerTwoReady) PlayerAttackUI[1].SetActive(false);
        if (!PlayerThreeReady) PlayerAttackUI[2].SetActive(false);
        MakeHitNote();
    }

    void MakeHitNote()
    {
        for (int i = 0; i < 3; i++)
        {
            if (PlayerAttackUI[i].activeSelf)
            {
                int DelayTime = Random.Range(0, 3);
                Debug.Log(DelayTime);
                GameObject note = Instantiate(HitNote, transform.position, Quaternion.identity, PlayerAttackUI[i].transform);
                RectTransform rect = note.GetComponent<RectTransform>();
                rect.anchoredPosition3D = new Vector3(transform.position.x + 800,0);
                rect.anchoredPosition3D += new Vector3((float)DelayTime, 0) * 100;

                NoteManager notecomp = note.GetComponent<NoteManager>();
                notecomp.TargetTransform = PlayerRectTransform[i];
                notecomp.PlayerfightUI = this;
                Notes.Add(notecomp);
            }
        }
    }

    public void CheckNotes(NoteManager note)
    {
        Notes.Remove(note);
        Destroy(note.gameObject);
        if (Notes.Count <= 0) { trunManage.IsPlayerTurn = false; gameObject.SetActive(false); }
    }
}
