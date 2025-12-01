using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerfightUI : MonoBehaviour
{
    public bool PlayerOneReady = false;
    public bool PlayerTwoReady = false;
    public bool PlayerThreeReady = false;

    public int NoteTurn = 0;

    public GameObject[] PlayerAttackUI;
    private RectTransform[] PlayerRectTransform;
    public NoteScan[] noteScans;
    public GameObject HitNote;
    public List<NoteManager> Notes;
    public TrunManage trunManage;
    private bool MadeFightNote;
    private bool CheckNote;
    public PlayerTurnUI turnUI;

    public List<bool> isDupe;

    private void Awake()
    {
        PlayerRectTransform = new RectTransform[PlayerAttackUI.Length];
        noteScans = new NoteScan[PlayerAttackUI.Length];
        for (int i = 0; i < PlayerAttackUI.Length; i++)
        {
            PlayerRectTransform[i] = PlayerAttackUI[i].GetComponent<RectTransform>();
            noteScans[i] = PlayerAttackUI[i].GetComponent<NoteScan>();
        }
    }

    public void ResetAttackData()
    {
        NoteTurn = 0;
        MadeFightNote = false;
        PlayerOneReady = false;
        PlayerTwoReady = false;
        PlayerThreeReady = false;
        for (int i = 0; i < noteScans.Length; i++)
        {
            noteScans[i].ResetDetect();
            PlayerAttackUI[i].SetActive(true);
        }
    }

    public void MakeFightList()
    {
        if (MadeFightNote) return;
        if (!PlayerOneReady && !PlayerTwoReady && !PlayerThreeReady) return;
        MadeFightNote = true;
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
                GameObject note = Instantiate(HitNote, transform.position, Quaternion.identity, PlayerAttackUI[i].transform);
                RectTransform rect = note.GetComponent<RectTransform>();
                noteScans[i].noteObj = rect;
                rect.anchoredPosition3D = new Vector3(transform.position.x + 800, 0);
                rect.anchoredPosition3D += new Vector3((float)DelayTime, 0) * 100;

                NoteManager notecomp = note.GetComponent<NoteManager>();
                noteScans[i].noteManager = notecomp;
                notecomp.NoteList = DelayTime;
                notecomp.TargetTransform = PlayerRectTransform[i];
                notecomp.PlayerfightUI = this;
                notecomp.PlayerType = i;
                Notes.Add(notecomp);
            }
        }
        FixNoteList();
    }

    public void FixNoteList()
    {
        var distinctSorted = new List<int>();

        foreach (var note in Notes)
        {
            if (!distinctSorted.Contains(note.NoteList))
                distinctSorted.Add(note.NoteList);
        }

        distinctSorted.Sort();

        foreach (var note in Notes)
        {
            note.NoteList = distinctSorted.IndexOf(note.NoteList);
        }
    }

    public void CheckNotes(NoteManager note)
    {
        Notes.Remove(note);
        Destroy(note.gameObject);
        if (Notes.Count <= 0) { trunManage.IsPlayerTurn = false; gameObject.SetActive(false); }
    }

    public void RemoveDupeNote(int playerType)
    {
        isDupe[playerType] = true;
        GameObject removeNote = Notes[0].gameObject;
        int type = 0;
        for(int i = Notes.Count -1; i>=0; i--)
        {
            if (Notes[i].PlayerType == playerType)
            {
                removeNote = Notes[i].gameObject;
                type = i;
            }
        }
        Notes.RemoveAt(type);
        Destroy(removeNote);
        //if(CheckNote) return;
        CheckNote = true;
        for (int i = Notes.Count -1; i >= 0 ; i--)
        {
            if (Notes[i].NoteList == NoteTurn)
            {
                Debug.LogWarning(Notes[i].PlayerType);
                if(Notes[i].PlayerType == 0 && PlayerOneReady) isDupe[0] = true;
                if(Notes[i].PlayerType == 1 && PlayerTwoReady) isDupe[1] = true;
                if(Notes[i].PlayerType == 2 && PlayerThreeReady) isDupe[2] = true;
                removeNote = Notes[i].gameObject;
                Notes.RemoveAt(i);
                Destroy(removeNote);
            }
        }
        FightEnemy(playerType);
        NoteTurn++;
        CheckNote = false;
        if (Notes.Count <= 0) Invoke("EndTurn", 1);
    }

    public void FightEnemy(int playerType)
    {
        List<bool> dupeData = isDupe;
        turnUI.AttackEnemy(playerType, dupeData);
    }

    public void EndTurn()
    {
        trunManage.StartEnemyTurn();
        gameObject.SetActive(false);
        isDupe = new List<bool> { false,false,false };
    }
}
