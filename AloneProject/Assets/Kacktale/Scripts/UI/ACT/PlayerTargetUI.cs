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
    }

    void CreatePlayerTarget()
    {
        int CountPlayer = PlayerTarget.PlayerTypes.Count() -1;
        for (int i = 0; i < CountPlayer; i++)
        {
            var target = Instantiate(TargetObj, transform.position, Quaternion.identity, ParantUI);
            TextMeshProUGUI text = target.GetComponent<TextMeshProUGUI>();
            TargetText.Add(text);
            TargetHP = target.GetComponentInChildren<Slider>();

            TargetText[i].text = PlayerTarget.PlayerTypes[i].Name;
            TargetHP.maxValue = PlayerTarget.PlayerTypes[i].MaxHp;
            TargetHP.value = PlayerTarget.PlayerTypes[i].Hp;
            TargetList.Add(target) ;
        }
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
                    //PlayerItemUI
                    break;
                default:
                    PlayerTurnUI.canSelect = true;
                    break;
            }
        }
    }

    public void CloseTargetUI()
    {
        for (int i = 0; i < TargetList.Count; i++)
        {
            Destroy(TargetList[i].gameObject);
            TargetList.Remove(TargetList[i]);
            TargetText.Remove(TargetText[i]);
        }
        Pannel.SetActive(false);
    }

}
