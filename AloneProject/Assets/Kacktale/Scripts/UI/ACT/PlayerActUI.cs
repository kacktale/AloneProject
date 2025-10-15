using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

[System.Serializable]
public class ActClass
{
    public string Name;
    public string Description;

    public bool P2Need;
    public bool P3Need;

    public int NeedTp;

    public bool isTargetEnemy = true;
}
[System.Serializable]
public class PlayerActList
{
    public ActClass[] ActClass;
}

public class PlayerActUI : ActControll
{
    [Header("고유 스킬들")]
    public int PlayerNum;
    public PlayerActList[] PlayerActName;
    [Header("텍스트 및 오브젝트들")]
    public GameObject ActTxt;
    public Transform TextArea;
    private TextMeshProUGUI SkillName;
    public TextMeshProUGUI SkillDescription;
    public TextMeshProUGUI NeedTpName;
    [Header("오브젝트 나열")]
    public List<GameObject> PlayerActlist = new List<GameObject>();
    private List<GameObject> playerObject = new List<GameObject>();
    public List<TextMeshProUGUI> ActTextlist = new List<TextMeshProUGUI>();
    [Header("필요한 컴포넌트들")]
    public PlayerTurnUI PlayerTurnUI;
    public PlayerTargetUI PlayerTargetUI;
    public TpManager TpManager;


    public void CreateActUI()
    {
        this.gameObject.SetActive(true);
        for (int i = 0; i < PlayerActName[PlayerNum].ActClass.Length; i++)
        {
            var act = Instantiate(ActTxt, transform.position, Quaternion.identity, TextArea);
            act.name = PlayerActName[PlayerNum].ActClass[i].Name + "Skill";
            PlayerActlist.Add(act);

            SkillName = act.GetComponent<TextMeshProUGUI>();
            foreach (Transform PlayerImg in act.transform)
            {
                playerObject.Add(PlayerImg.gameObject);
            }

            if (PlayerActName[PlayerNum].ActClass[i].P2Need) playerObject[0].SetActive(true);
            if (PlayerActName[PlayerNum].ActClass[i].P3Need) playerObject[1].SetActive(true);

            SkillName.text = PlayerActName[PlayerNum].ActClass[i].Name;
            playerObject.Clear();

            TextMeshProUGUI actTxt = act.GetComponent<TextMeshProUGUI>();
            ActTextlist.Add(actTxt);
            if (PlayerActName[PlayerNum].ActClass[i].NeedTp > TpManager.Tp) ActTextlist[ActNum].color = Color.gray;
            else ActTextlist[ActNum].color = Color.yellow;

        }
        SkillDescription.text = PlayerActName[PlayerNum].ActClass[ActNum].Description;
        NeedTpName.text = $"{PlayerActName[PlayerNum].ActClass[ActNum].NeedTp}% TP";
    }

    public override void ChangeAct(int value, List<TextMeshProUGUI> textList)
    {
        base.ChangeAct(value, textList);
        ChangeDec();
    }

    public void Update()
    {
        if (IsMyturn)
        {
            ChooseAct(ActTextlist);
            SelectAct();
            if (Input.GetKeyDown(KeyCode.X))
            {
                ExitChooseAct();
                PlayerTurnUI.SetActSelect();
            }
        }
    }

    void ChangeDec()
    {
        SkillDescription.text = PlayerActName[PlayerNum].ActClass[ActNum].Description;
        NeedTpName.text = $"{PlayerActName[PlayerNum].ActClass[ActNum].NeedTp}% TP";
    }

    void SelectAct()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (TpManager.Tp < PlayerActName[PlayerNum].ActClass[ActNum].NeedTp) return;
            TpManager.Tp -= PlayerActName[PlayerNum].ActClass[ActNum].NeedTp;
            ExitChooseAct();
            PlayerTurnUI.PlayerAct[PlayerNum].ActDetail = ActNum;
            PlayerTargetUI.BeforeACTNum = 1;
            PlayerTargetUI.CreateTarget(PlayerActName[PlayerNum].ActClass[ActNum].isTargetEnemy);
            PlayerTargetUI.IsMyturn = true;
            PlayerTargetUI.PlayerType = PlayerNum;
        }
    }
    public void ExitChooseAct()
    {
        ActNum = 0;
        IsMyturn = false;

        for (int i = 0; i < ActTextlist.Count; i++)
        {
            Destroy(PlayerActlist[i]);
        }

        PlayerActlist.Clear();
        playerObject.Clear();
        ActTextlist.Clear();

        SkillDescription.text = "";
        NeedTpName.text = "";
        this.gameObject.SetActive(false);
    }
}
