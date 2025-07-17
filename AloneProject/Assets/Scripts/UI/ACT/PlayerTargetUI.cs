using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTargetUI : MonoBehaviour
{
    public GameObject Pannel;

    public EnemyType[] EnemyTarget;
    public Players PlayerTarget;

    public Transform ParantUI;
    public GameObject TargetObj;

    public PlayerActUI PlayerActUI;
    public PlayerItemUI PlayerItemUI;
    public PlayerTurnUI PlayerTurnUI;

    public int ACTNum; //전 행동 ui

    private TextMeshProUGUI TargetText;
    private Slider TargetHP;

    private List<GameObject> TargetList = new List<GameObject>();

    public List<GameObject> ShowList => TargetList;
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
            TargetText = target.GetComponent<TextMeshProUGUI>();
            TargetHP = target.GetComponentInChildren<Slider>();

            TargetText.text = EnemyTarget[i].Name;
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
            TargetText = target.GetComponent<TextMeshProUGUI>();
            TargetHP = target.GetComponentInChildren<Slider>();

            TargetText.text = PlayerTarget.PlayerTypes[i].Name;
            TargetHP.maxValue = PlayerTarget.PlayerTypes[i].MaxHp;
            TargetHP.value = PlayerTarget.PlayerTypes[i].Hp;
            TargetList.Add(target) ;
        }
    }

    public void CloseTargetUI()
    {
        for (int i = 0; i < TargetList.Count; i++)
        {
            Destroy(TargetList[i].gameObject);
            TargetList.Remove(TargetList[i]);
        }
        Pannel.SetActive(false);
        switch (ACTNum)
        {
            case 1:
                PlayerActUI.CreateActUI();
                break;
            case 2:
                //PlayerItemUI
                break;
            default:
                PlayerTurnUI.GotoNextPerson(); 
                break;
        }
    }
}
