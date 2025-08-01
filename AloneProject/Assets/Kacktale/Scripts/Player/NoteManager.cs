using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public RectTransform TargetTransform;
    private RectTransform NoteTransform;
    public PlayerfightUI PlayerfightUI;
    public int DamageValue;
    public float Speed;

    private void Awake()
    {
        NoteTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        NoteTransform.anchoredPosition3D -= Vector3.right * Speed;
        if (NoteTransform.anchoredPosition3D.x <= -100f) PlayerfightUI.CheckNotes(this);
    }
}
