using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerActEtc
{
    public int ActNum = 0; // 0 : Fight | 1 : ACT | 2: ITEM | 3: MERCY | 4: DEFENCE | 5: Skiped
    public int ActDetail = 0;
    public string ItemName = "";
    public int HealTarget;
    public int HealAmount;
    public Image[] ACTImage;
}

public class PlayerTurnUI : MonoBehaviour
{
    #region 변수들
    public Players playerData;
    public EnemyType[] enemyType;
    public TrunManage TrunManage;
    public RectTransform[] PlayerUI;
    public PlayerActEtc[] PlayerAct;
    public int PlayerActType = 0;
    public PlayerHpUI PlayerHpUI;

    public bool canSelect = true;
    private bool resultTurn = false;

    public PlayerActUI PlayerActUI;
    public PlayerfightUI playerfightUI;
    public PlayerTargetUI playerTargetUI;
    public PlayerItemUI playerItemUI;
    public TpManager playerTpManager;
    public PlayerHpUI playerHpUI;

    public bool SkipP2 = false;
    public bool SkipP3 = false;

    public TextMeshProUGUI DescriptionText;
    #endregion
    [SerializeField] private int DetailTurn = 0;
    public DamageText[] DamageText;

    public List<bool> playerAttacked;
    public List<bool> playerKnockOut;

    void Start()
    {
        AppearACTUI();
        PlayerHpUI = gameObject.GetComponent<PlayerHpUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TrunManage.IsPlayerTurn && canSelect && !resultTurn)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) ChangeAct(-1);
            if (Input.GetKeyDown(KeyCode.RightArrow)) ChangeAct(1);

            CreateUI(); //ui 만들기
            GotoBeforePlayer(); //ui 닫기
        }
        if (TrunManage.IsPlayerTurn && resultTurn)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                DetailTurn++;
                if (DetailTurn <= 2) PrintDetailText();
                else CheckFight();
            }
        }
    }

    void CheckFight()
    {
        DescriptionText.gameObject.SetActive(false);
        if (PlayerAct.Any(p => p.ActNum == 0))
        {
            canSelect = false;
            resultTurn = false;

            playerfightUI.gameObject.SetActive(true);
            if (PlayerAct[0].ActNum == 0) playerfightUI.PlayerOneReady = true;
            if (PlayerAct[1].ActNum == 0) playerfightUI.PlayerTwoReady = true;
            if (PlayerAct[2].ActNum == 0) playerfightUI.PlayerThreeReady = true;
            playerfightUI.MakeFightList();
        }
        else
        {
            resultTurn = false;
            TrunManage.StartEnemyTurn();
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
        if (PlayerActType == 0 && playerKnockOut[0]) GotoNextPlayer();
        PlayerUI[PlayerActType].anchoredPosition = new Vector2(0, 30);
    }
    void DisappearACTUI()
    {
        PlayerUI[PlayerActType].anchoredPosition = new Vector2(0, 0);
    }
    #endregion

    #region 시스템
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
            playerTpManager.UpdateTp(20);
            GotoNextPlayer();
        }
    }
    public void GotoNextPlayer()
    {
        //playerTargetUI.CloseTargetUI();
        playerItemUI.PlayerTurn++;

        DisappearACTUI();

        if (PlayerActType >= 2 || (SkipP2 && SkipP3) || (PlayerActType == 1 && SkipP3) || (playerKnockOut[1] && playerKnockOut[2]) || (PlayerActType == 1 && playerKnockOut[2]))
        {
            canSelect = false;
            resultTurn = true;
            ShowResault();
        }
        else
        {
            canSelect = true;
            PlayerActType = (PlayerActType + 4) % 3;
            if (SkipP2) PlayerActType++;
            if(playerKnockOut[1]) PlayerActType++;
            AppearACTUI();
        }
    }
    public void GotoBeforePlayer()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (PlayerActType - 1 == playerItemUI.FirstItemPlayer) playerItemUI.FirstItemAct = true;
            if (playerItemUI.IsConnected)
            {
                if (playerItemUI.PlayerTurn > 0) playerItemUI.PlayerTurn--;
                playerItemUI.ShowItem();
            }

            TpBack();

            DisappearACTUI();
            if (PlayerActType > 0) PlayerActType = (PlayerActType + 2) % 3;
            if (SkipP2) PlayerActType--;
            if (PlayerActType == 1 && playerKnockOut[1]) PlayerActType--;
            SkipP2 = false;
            SkipP3 = false;
            AppearACTUI();
        }
    }

    void TpBack()
    {
        if (PlayerActType >= 1 && PlayerAct[PlayerActType - 1].ActNum == 4)
        {
            playerTpManager.UpdateTp(-20);
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
        //TrunManage.IsPlayerTurn = false;
        DescriptionText.gameObject.SetActive(true);
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
                    case 5:
                        break;
                    default:
                        DetailTurn++;
                        PrintDetailText();
                        return;
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
                    case 5:
                        break;
                    default:
                        DetailTurn++;
                        PrintDetailText();
                        return;
                }
                break;
            case 2:
                switch (PlayerAct[2].ActNum)
                {
                    case 1:
                        switch (PlayerAct[2].ActDetail)
                        {
                            case 0:
                                DescriptionText.text = SkillText.Heal + $"{PlayerActUI.PlayerActName[2].ActClass[0].Name}!";
                                break;
                            case 1:
                                DescriptionText.text = SkillText.Sleep + $"{PlayerActUI.PlayerActName[2].ActClass[1].Name}!";
                                break;
                        }
                        break;
                    case 2:
                        DescriptionText.text = HealText.Player3 + $"{PlayerAct[2].ItemName}!";
                        break;
                    case 3:
                        DescriptionText.text = SpareText.Player3 + $"{playerTargetUI.EnemyTarget[PlayerAct[2].ActDetail]}!";
                        if (playerTargetUI.EnemyTarget[PlayerAct[2].ActDetail].Tired < 100) DescriptionText.text += SpareText.failedSpare;
                        break;
                    case 5:
                        break;
                    default:
                        DescriptionText.gameObject.SetActive(false);
                        CheckFight();
                        return;
                }
                break;
        }
        PlayAct();
    }

    void PlayAct()
    {
        switch (DetailTurn)
        {
            case 0:
                switch (PlayerAct[0].ActNum)
                {
                    case 1:
                        switch (PlayerAct[0].ActDetail)
                        {
                            case 0: Debug.Log("액트 1"); break;
                            case 1: Debug.Log("액트 2"); break;
                            case 2: Debug.Log("액트 3"); break;
                        }
                        break;
                    case 2:
                        playerData.PlayerTypes[PlayerAct[0].HealTarget + 1].Hp += PlayerAct[0].HealAmount;
                        DamageText[3].TextHeal(PlayerAct[0].HealAmount);
                        PlayerHpUI.UpdateUI();
                        break;
                    case 3:
                        if (playerTargetUI.EnemyTarget[PlayerAct[0].ActDetail].Tired >= 100)
                        {

                        }
                        break;
                    case 5:
                        break;
                    default:
                        PrintDetailText();
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
                        playerData.PlayerTypes[PlayerAct[1].HealTarget + 1].Hp += PlayerAct[1].HealAmount;
                        DamageText[4].TextHeal(PlayerAct[1].HealAmount);
                        PlayerHpUI.UpdateUI();
                        break;
                    case 3:
                        if (playerTargetUI.EnemyTarget[PlayerAct[1].ActDetail].Tired >= 100)
                        {

                        }
                        break;
                    case 5:
                        break;
                    default:
                        PrintDetailText();
                        break;
                }
                break;
            case 2:
                switch (PlayerAct[2].ActNum)
                {
                    case 1:
                        switch (PlayerAct[2].ActDetail)
                        {
                            case 0:
                                DescriptionText.text = SkillText.Heal + $"{PlayerActUI.PlayerActName[2].ActClass[0].Name}!";
                                break;
                            case 1:
                                DescriptionText.text = SkillText.Sleep + $"{PlayerActUI.PlayerActName[2].ActClass[1].Name}!";
                                break;
                        }
                        break;
                    case 2:
                        Debug.Log("Turn : " + DetailTurn);
                        Debug.Log("Before : " + playerData.PlayerTypes[PlayerAct[2].HealTarget + 1].Hp);
                        playerData.PlayerTypes[PlayerAct[2].HealTarget + 1].Hp += PlayerAct[2].HealAmount;
                        DamageText[5].TextHeal(PlayerAct[2].HealAmount);
                        PlayerHpUI.UpdateUI();
                        Debug.Log("After : " + playerData.PlayerTypes[PlayerAct[2].HealTarget + 1].Hp);
                        break;
                    case 3:
                        if (playerTargetUI.EnemyTarget[PlayerAct[2].ActDetail].Tired >= 100)
                        {

                        }
                        break;
                    case 5:
                        break;
                    default:
                        DescriptionText.gameObject.SetActive(false);
                        CheckFight();
                        break;
                }
                break;
        }
    }

    #endregion

    public void StartAct()
    {
        PlayerActType = 0;
        AppearACTUI();
        canSelect = true;
        foreach (var act in PlayerAct)
        {
            act.ActNum = 0;
            act.ActDetail = 0;
            act.ItemName = null;
            act.HealTarget = 0;
            act.HealAmount = 0;
            for (int i = 0; i < act.ACTImage.Length; i++) act.ACTImage[i].color = Color.white;
            act.ACTImage[act.ActNum].color = Color.yellow;
        }
        for (int i = 0; i < PlayerAct.Length; i++)
        {
            if (playerKnockOut[i]) PlayerAct[i].ActNum = 5;
        }
        for (int i = 0; i < playerAttacked.Count; i++) playerAttacked[i] = false;
        playerfightUI.ResetAttackData();
        SkipP2 = false;
        SkipP3 = false;
        DetailTurn = 0;
    }

    public void AttackEnemy(int type, List<bool> dupeList)
    {
        int Damage = playerData.PlayerTypes[type + 1].ATK - enemyType[PlayerAct[type].ActDetail].Def;
        if (Damage <= 0) Damage = 1;

        if (!playerAttacked[type])
        {
            playerAttacked[type] = true;
            enemyType[PlayerAct[type].ActDetail].Hp -= Damage;
            Debug.Log("플레이어" + type + "데미지 :" + Damage);
            DamageText[type].FixTextValue(Damage);
        }
        int dupeType = 0;

        for (int dupe = 0; dupe < dupeList.Count; dupe++)
        {
            bool dupeCheck = dupeList[dupe] && dupe != type;
            if (dupeCheck) dupeType = dupe;

            if (dupeCheck && !playerAttacked[dupeType])
            {
                playerAttacked[dupeType] = true;

                Damage = playerData.PlayerTypes[dupeType + 1].ATK - enemyType[PlayerAct[dupeType].ActDetail].Def;
                if (Damage <= 0) Damage = 1;
                enemyType[PlayerAct[dupeType].ActDetail].Hp -= Damage;
                Debug.Log(" 중복 플레이어" + dupeType + "데미지 :" + Damage);
                DamageText[dupeType].FixTextValue(Damage);
            }
        }
    }

    public void RandomPlayerDamage()
    {
        if (playerData.PlayerTypes[1].Hp >= 0 || playerData.PlayerTypes[2].Hp >= 0 || playerData.PlayerTypes[3].Hp >= 0)
        {
            int type = Random.Range(1, 4);
            type = CheckPlayerKnockOut(type);
            int Damage = Random.Range(30, 34) - playerData.PlayerTypes[type].Def;

            if (Damage <= 0) Damage = 1;

            playerData.PlayerTypes[type].Hp -= Damage;
            if(playerData.PlayerTypes[type].Hp < 0) DamageText[type + 2].TextDown();
            else DamageText[type + 2].FixTextValue(Damage,false);
            playerHpUI.UpdateUI();
        }
        else Debug.LogErrorFormat("게임 끝");
    }

    int CheckPlayerKnockOut(int type)
    {
        type--;
        while (playerKnockOut[type])
        {
            type = (type + 1) % 3;
        }
        type++;
        return type;
    }
}
