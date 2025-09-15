using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TpManager : MonoBehaviour
{
    [Range(0,100)]
    public int Tp;
    public Slider TpSlider;
    public TextMeshProUGUI TpText;

    public void UpdateTp(int Value)
    {
        Tp += Value;
        if(Tp >= 100) Tp = 100;
        TpSlider.value = Tp;
        TpText.text = $"{Tp}\n%";
    }
}
