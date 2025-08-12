using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScan : MonoBehaviour
{
    public int PlayerType;
    public bool closeNote;
    public bool correctNote;
    public RectTransform noteObj;
    public NoteManager noteManager;
    public PlayerfightUI playerfightUI;
    // Update is called once per frame
    void Update()
    {
        if (!closeNote) NoteDetect();
        if (Input.GetKeyDown(KeyCode.Z) && closeNote && noteObj != null && noteManager.NoteList == playerfightUI.NoteTurn)
        {
            fightDetect();
            FinalDamage();
        }
    }

    void NoteDetect()
    {
        if (noteObj.anchoredPosition3D.x < 100f && noteObj.anchoredPosition3D.x > 0f) closeNote = true;
    }

    void fightDetect()
    {
        if (noteObj.anchoredPosition3D.x < 10f && noteObj.anchoredPosition3D.x > 0f) correctNote = true;
    }

    void FinalDamage()
    {
        playerfightUI.RemoveDupeNote(PlayerType);
    }
}
