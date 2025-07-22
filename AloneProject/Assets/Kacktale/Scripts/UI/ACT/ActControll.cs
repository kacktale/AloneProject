using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActControll : MonoBehaviour
{
    public int ActNum;
    public bool IsMyturn;

    public void ChooseAct(List<TextMeshProUGUI> textList)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) ChangeAct(-2, textList);
        if (Input.GetKeyDown(KeyCode.DownArrow)) ChangeAct(2, textList);
        if (Input.GetKeyDown(KeyCode.RightArrow)) ChangeAct(1, textList);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) ChangeAct(-1, textList);
    }
    public virtual void ChangeAct(int value, List<TextMeshProUGUI> textList)
    {
        if (ActNum + value < 0 || ActNum + value > textList.Count - 1) return;
        ActNum += value;
        for (int i = 0; i < textList.Count; i++)
        {
            if (i == ActNum) textList[i].color = Color.yellow;
            else textList[i].color = Color.white;
        }
    }
}
