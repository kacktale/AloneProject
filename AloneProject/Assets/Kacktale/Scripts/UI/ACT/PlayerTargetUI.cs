using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTargetUI : ActControll
{
    public GameObject Pannel;

    public EnemyType[] EnemyTarget;
    public Players PlayerTarget;

    public Transform ParantUI;
    public GameObject TargetObj;

    public PlayerActUI PlayerActUI;
    public PlayerItemUI PlayerItemUI;
    public PlayerTurnUI PlayerTurnUI;

    public int BeforeACTNum; //전 행동 ui

    private Slider TargetHP;

    private List<GameObject> TargetList = new List<GameObject>();
    private List<TextMeshProUGUI> TargetText = new List<TextMeshProUGUI>();

    public List<GameObject> ShowList => TargetList;
    public override void ChooseAct(List<TextMeshProUGUI> textList)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) ChangeAct(-1, textList);
        if (Input.GetKeyDown(KeyCode.DownArrow)) ChangeAct(1, textList);
    }

    public void Update()
    {
        if (IsMyturn)
        {
            ChooseAct(TargetText);
            SelectTarget();
            GotoBeforeAct();
        }
    }


    public void CreateTarget(bool isTargetEnemy)
    {
        Pannel.SetActive(true);
        if (isTargetEnemy) CreateEnemyTarget();
        else CreatePlayerTarget();
    }

    void CreateEnemyTarget()
    {
        int CountEnemys = EnemyTarget.Count();
        for(int i = 0; i < CountEnemys; i++)
        {
            GameObject target = Instantiate(TargetObj, transform.position, Quaternion.identity, ParantUI);
            TextMeshProUGUI text = target.GetComponent<TextMeshProUGUI>();
            TargetText.Add(text);
            TargetHP = target.GetComponentInChildren<Slider>();

            TargetText[i].text = EnemyTarget[i].Name;
            TargetHP.maxValue = EnemyTarget[i].MaxHp;
            TargetHP.value = EnemyTarget[i].Hp;
            TargetList.Add(target);
        }
        TargetText[0].color = Color.yellow;
    }

    void CreatePlayerTarget()
    {
        int CountPlayer = PlayerTarget.PlayerTypes.Count();
        for (int i = 1; i < CountPlayer; i++)
        {
            var target = Instantiate(TargetObj, transform.position, Quaternion.identity, ParantUI);
            TextMeshProUGUI text = target.GetComponent<TextMeshProUGUI>();
            TargetHP = target.GetComponentInChildren<Slider>();
            TargetText.Add(text);

            TargetText[i - 1].text = PlayerTarget.PlayerTypes[i].Name;

            TargetHP.maxValue = PlayerTarget.PlayerTypes[i].MaxHp;
            TargetHP.value = PlayerTarget.PlayerTypes[i].Hp;

            TargetList.Add(target);
        }
        TargetText[0].color = Color.yellow;
    }

    void SelectTarget()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CloseTargetUI();
            PlayerTurnUI.GotoNextPlayer();
            PlayerTurnUI.canSelect = true;
        }
    }

    void GotoBeforeAct()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            IsMyturn = false;
            CloseTargetUI();
            switch (BeforeACTNum)
            {
                case 1:
                    PlayerActUI.CreateActUI();
                    PlayerActUI.IsMyturn = true;
                    break;
                case 2:
                    PlayerItemUI.CreateItemList();
                    PlayerItemUI.IsMyturn = true;
                    break;
                default:
                    PlayerTurnUI.SetActSelect();
                    break;
            }
        }
    }

    public void CloseTargetUI()
    {
        for (int i = 0; i < TargetList.Count; i++) Destroy(TargetList[i].gameObject);
        TargetList.Clear();
        TargetText.Clear();
        Pannel.SetActive(false);
    }

}
