using System.Collections;
using System.Collections.Generic;
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
}

public class PlayerActUI : MonoBehaviour
{
    public ActClass[] ActClass;
    public ActClass[] P2ActClass;
    public ActClass[] P3ActClass;
    public GameObject ActTxt;
    public Transform ParantObj;
    private TextMeshProUGUI SkillName;
    public TextMeshProUGUI SkillDescription;
    public TextMeshProUGUI NeedTpName;
    private List<GameObject> playerObject = new List<GameObject>();
    public List<GameObject> PlayerActlist = new List<GameObject>();

    public void CreateActUI()
    {
        for(int i = 0; i < ActClass.Length; i++)
        {
            var act = Instantiate(ActTxt,transform.position,Quaternion.identity, ParantObj);
            act.name = ActClass[i].Name + "Skill";
            PlayerActlist.Add(act); 

            SkillName = act.GetComponent<TextMeshProUGUI>();
            foreach(Transform PlayerImg in act.transform)
            {
                playerObject.Add(PlayerImg.gameObject);
            }

            if(ActClass[i].P2Need) playerObject[0].SetActive(true);
            if(ActClass[i].P3Need) playerObject[1].SetActive(true);

            SkillName.text = ActClass[i].Name;
            playerObject.Clear();
        }
            SkillDescription.text = ActClass[0].Description;
            NeedTpName.text = $"{ActClass[0].NeedTp}% TP";
    }
}
