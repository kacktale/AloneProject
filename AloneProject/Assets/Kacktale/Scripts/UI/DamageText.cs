using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI damageShowText;
    public Animator animator;

    private const string hitAnimTriggerName = "IsHit";
    public void FixTextValue(int DamageValue, bool hitByPlayer = true)
    {
        animator.SetTrigger(hitAnimTriggerName);
        if(!hitByPlayer) damageShowText.color = Color.white;
        damageShowText.text = DamageValue.ToString();
    }

    public void TextDown()
    {
        animator.SetTrigger(hitAnimTriggerName);
        damageShowText.color = Color.red;
        damageShowText.text = "Down";
    }

    public void TextHeal(int healValue)
    {
        animator.SetTrigger(hitAnimTriggerName);
        damageShowText.color = Color.green;
        damageShowText.text = "+" + healValue.ToString();
    }
}
