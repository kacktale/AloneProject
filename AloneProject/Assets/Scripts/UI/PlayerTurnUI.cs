using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerActEtc
{
    public int ActNum = 0;
    public Image[] ACTImage;
}

public class PlayerTurnUI : MonoBehaviour
{
    public TrunManage TrunManage;
    public RectTransform[] PlayerUI;
    public PlayerActEtc[] PlayerAct;
    public int PlayerActType = 0;

    private void Start()
    {
        AppearACTUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) ChangeAct(-1);
        if (Input.GetKeyDown(KeyCode.RightArrow)) ChangeAct(1);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            DisappearACTUI();
            PlayerActType = (PlayerActType + 4) % 3;
            AppearACTUI();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            DisappearACTUI();
            if(PlayerActType != 0)
            {
                PlayerActType--;
            }
            AppearACTUI();
        }
    }

    void ChangeAct(int value)
    {
        if (TrunManage.IsPlayerTurn)
        {
            switch (PlayerActType)
            {
                case 0:
                    PlayerAct[0].ActNum = (PlayerAct[0].ActNum + value + 5) % 5;
                    for (int i = 0; i < PlayerAct[0].ACTImage.Length; i++)
                    {
                        if (i == PlayerAct[0].ActNum) PlayerAct[0].ACTImage[i].color = Color.yellow;
                        else PlayerAct[0].ACTImage[i].color = Color.white;
                    }
                    break;
                case 1:
                    PlayerAct[1].ActNum = (PlayerAct[1].ActNum + value + 5) % 5;
                    for (int i = 0; i < PlayerAct[1].ACTImage.Length; i++)
                    {
                        if (i == PlayerAct[1].ActNum) PlayerAct[1].ACTImage[i].color = Color.yellow;
                        else PlayerAct[1].ACTImage[i].color = Color.white;
                    }
                    break;
                case 2:
                    PlayerAct[2].ActNum = (PlayerAct[2].ActNum + value + 5) % 5;
                    for (int i = 0; i < PlayerAct[2].ACTImage.Length; i++)
                    {
                        if (i == PlayerAct[2].ActNum) PlayerAct[2].ACTImage[i].color = Color.yellow;
                        else PlayerAct[2].ACTImage[i].color = Color.white;
                    }
                    break;
            }
        }
    }

    void AppearACTUI()
    {
        PlayerUI[PlayerActType].anchoredPosition = new Vector2(0, 30);
    }
    void DisappearACTUI()
    {
        PlayerUI[PlayerActType].anchoredPosition = new Vector2(0, 0);
    }
}
