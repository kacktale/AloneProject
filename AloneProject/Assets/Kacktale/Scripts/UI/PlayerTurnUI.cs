using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerActEtc
{
    public int ActNum = 0; // 0 : Fight | 1 : ACT | 2: ITEM | 3: MERCY | 4: DEFENCE
    public int ActDetail = 0;
    public string ItemName = "";
    public int HealTarget;
    public int HealAmount;
    public Image[] ACTImage;
}

public class PlayerTurnUI : MonoBehaviour
{
    #region 변수들
    public TrunManage TrunManage;
    public RectTransform[] PlayerUI;
    public PlayerActEtc[] PlayerAct;
    public int PlayerActType = 0;

    private int EnemyLeft = 0;
    public bool canSelect = true;

    public PlayerActUI PlayerActUI;
    public PlayerfightUI playerfightUI;
    public PlayerTargetUI playerTargetUI;
    public PlayerItemUI playerItemUI;

    public TextMeshProUGUI DescriptionText;
    #endregion
    private int DetailTurn = 0;
    void Start()
    {
        AppearACTUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (TrunManage.IsPlayerTurn && canSelect)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) ChangeAct(-1);
            if (Input.GetKeyDown(KeyCode.RightArrow)) ChangeAct(1);

            CreateUI(); //ui 만들기
            GotoBeforePlayer(); //ui 닫기
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

    #region 연출
    void AppearACTUI()
    {
        PlayerUI[PlayerActType].anchoredPosition = new Vector2(0, 30);
    }
    void DisappearACTUI()
    {
        PlayerUI[PlayerActType].anchoredPosition = new Vector2(0, 0);
    }
    #endregion

    void CreateUI()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            DescriptionText.gameObject.SetActive(false);
            canSelect = false;
            if (PlayerAct[PlayerActType].ActNum == 1)
            {
                PlayerActUI.PlayerNum = PlayerActType;
                PlayerActUI.CreateActUI();
                PlayerActUI.IsMyturn = true;
                return;
            }
            else if (PlayerAct[PlayerActType].ActNum == 2)
            {
                if (playerItemUI.FirstItemAct) playerItemUI.FirstItemPlayer = PlayerActType;
                playerItemUI.PlayerNum = PlayerActType;
                playerItemUI.CreateItemList();
                playerItemUI.IsMyturn = true;
                return;
            }
            else if (PlayerAct[PlayerActType].ActNum != 4)
            {
                playerTargetUI.BeforeACTNum = 0;
                playerTargetUI.IsMyturn = true;
                playerTargetUI.CreateTarget(true);
                return;
            }
            GotoNextPlayer();
        }
    }

    #region 시스템
    public void GotoNextPlayer()
    {
        canSelect = true;
        //playerTargetUI.CloseTargetUI();
        playerItemUI.PlayerTurn++;

        DisappearACTUI();
        if (PlayerActType >= 2) ShowResault();
        else
        {
            PlayerActType = (PlayerActType + 4) % 3;
            AppearACTUI();
        }
    }
    public void GotoBeforePlayer()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (PlayerActType - 1 == playerItemUI.FirstItemPlayer) playerItemUI.FirstItemAct = true;
            if (playerItemUI.PlayerTurn > 0) playerItemUI.PlayerTurn--;

            playerItemUI.ShowItem();

            DisappearACTUI();
            if (PlayerActType > 0) PlayerActType = (PlayerActType + 2) % 3;
            AppearACTUI();
        }
    }

    public void SetActSelect()
    {
        canSelect = true;
        DescriptionText.gameObject.SetActive(true);
    }
    #endregion

    #region 플레이어 선택결과

    void ShowResault()
    {
        TrunManage.IsPlayerTurn = false;
        PrintDetailText();
    }

    void PrintDetailText()
    {
        switch (DetailTurn)
        {
            case 0:
                switch (PlayerAct[0].ActNum)
                {
                    case 1:
                        switch (PlayerAct[0].ActDetail)
                        {
                            case 0: DescriptionText.text = SkillText.spinOne; break;
                            case 1: DescriptionText.text = SkillText.spinTwo; break;
                            case 2: DescriptionText.text = SkillText.spinAll; break;
                        }
                        break;
                    case 2:
                        DescriptionText.text = HealText.Player1 + $"{PlayerAct[0].ItemName}!";
                        break;
                    case 3:
                        DescriptionText.text = SpareText.Player1 + $"{playerTargetUI.EnemyTarget[PlayerAct[0].ActDetail]}!";
                        if (playerTargetUI.EnemyTarget[PlayerAct[0].ActDetail].Tired < 100) DescriptionText.text += SpareText.failedSpare;
                        break;
                }
                break;
            case 1:
                switch (PlayerAct[1].ActNum)
                {
                    case 1:
                        switch (PlayerAct[1].ActDetail)
                        {
                            case 0:
                                DescriptionText.text = SkillText.rudeBuster + $"{PlayerActUI.PlayerActName[1].ActClass[0].Name}!";
                                break;
                        }
                        break;
                    case 2:
                        DescriptionText.text = HealText.Player2 + $"{PlayerAct[1].ItemName}!";
                        break;
                    case 3:
                        DescriptionText.text = SpareText.Player2 + $"{playerTargetUI.EnemyTarget[PlayerAct[1].ActDetail]}!";
                        if (playerTargetUI.EnemyTarget[PlayerAct[1].ActDetail].Tired < 100) DescriptionText.text += SpareText.failedSpare;
                        break;
                }
                break;
            case 2:
                switch (PlayerAct[2].ActNum)
                {
                    case 1:
                        switch (PlayerAct[1].ActDetail)
                        {
                            case 0:
                                DescriptionText.text = SkillText.rudeBuster + $"{PlayerActUI.PlayerActName[1].ActClass[0].Name}!";
                                break;
                        }
                        break;
                    case 2:
                        DescriptionText.text = HealText.Player2 + $"{PlayerAct[1].ItemName}!";
                        break;
                    case 3:
                        DescriptionText.text = SpareText.Player2 + $"{playerTargetUI.EnemyTarget[PlayerAct[1].ActDetail]}!";
                        if (playerTargetUI.EnemyTarget[PlayerAct[1].ActDetail].Tired < 100) DescriptionText.text += SpareText.failedSpare;
                        break;
                }
                break;
        }
        DetailTurn++;
    }

    #endregion
}
