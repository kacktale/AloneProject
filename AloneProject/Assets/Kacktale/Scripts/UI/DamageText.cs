using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI damageShowText;
    public Animator animator;

    private const string hitAnimTriggerName = "IsHit";
    public void FixTextValue(int DamageValue)
    {
        animator.SetTrigger(hitAnimTriggerName);
        damageShowText.text = DamageValue.ToString();
    }
}
