using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpUI : MonoBehaviour
{
    public Players PlayerData;
    public TextMeshProUGUI[] PlayerName;
    public Slider[] PlayerHp;
    public int[] Type;
    public ErrorManager ErrorManager;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < PlayerName.Length; i++)
        {
            if (Type[i] <= 0) ErrorManager.ShowError(0);
            PlayerName[i].text = PlayerData.PlayerTypes[Type[i]].Name;
            PlayerHp[i].maxValue = PlayerData.PlayerTypes[Type[i]].MaxHp;
            PlayerHp[i].value = PlayerData.PlayerTypes[Type[i]].Hp;
        }
    }
}
