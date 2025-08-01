using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[System.Serializable]
public class ItemName
{
    public string itemName;
    public string description;
    public bool canRecive;
    public int healAmount;
}

public class PlayerItemUI : ActControll
{
    public ItemName[] itemManager;

    private List<ItemName> OriginalitemManager;
    private List<ItemName> CurrentItemManager;
    private List<ItemName> P3ItemManager;

    private List<ItemName> tempitemManager;

    private bool IsConnected = false;
    public int FirstItemPlayer = 0;
    public int PlayerTurn = 0;

    public bool FirstItemAct = true;
    public int PlayerNum;
    public int PlayerType;

    public GameObject itemPrefab;
    public Transform textArea;

    private TextMeshProUGUI itemText;
    public TextMeshProUGUI itemDescription;

    public PlayerTurnUI playerTurnUI;
    public PlayerTargetUI playerTargetUI;

    public List<TextMeshProUGUI> textColorManager;
    public List<TextMeshProUGUI> tempTexts;

    // Start is called before the first frame update
    void Start()
    {
        if (!IsConnected)
        {
            OriginalitemManager = new List<ItemName>(itemManager);
            tempitemManager = new List<ItemName>(itemManager);
            IsConnected = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMyturn)
        {
            ChooseAct(textColorManager);
            SelectItem();
            if (Input.GetKeyDown(KeyCode.X))
            {
                ExitItemList();
                playerTurnUI.SetActSelect();
            }
        }
    }

    public void CreateItemList()
    {
        this.gameObject.SetActive(true);

        if (itemManager.Count() < ActNum) ActNum = itemManager.Count();
        itemDescription.text = itemManager[ActNum].description;

        for (int i = 0; i < itemManager.Length; i++)
        {
            GameObject itemList = Instantiate(itemPrefab, transform.position, Quaternion.identity, textArea);
            itemText = itemList.GetComponent<TextMeshProUGUI>();
            itemText.text = itemManager[i].itemName;
            textColorManager.Add(itemText);
        }
        textColorManager[0].color = Color.yellow;
    }

    void SelectItem()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            playerTurnUI.PlayerAct[PlayerNum].ActDetail = ActNum;

            playerTurnUI.PlayerAct[PlayerNum].ItemName = itemManager[PlayerNum].itemName;
            playerTurnUI.PlayerAct[PlayerNum].HealAmount = itemManager[PlayerNum].healAmount;

            HideItem();

            playerTargetUI.PlayerType = PlayerType;
            playerTargetUI.BeforeACTNum = 2;
            playerTargetUI.IsMyturn = true;
            playerTargetUI.CreateTarget(false);

            ExitItemList();
        }
    }

    void ExitItemList()
    {
        IsMyturn = false;
        itemDescription.text = "";
        for (int i = 0; i < textColorManager.Count; i++) Destroy(textColorManager[i].gameObject);
        textColorManager.Clear();
        this.gameObject.SetActive(false);
    }

    void HideItem()
    {
        Debug.Log("재생1");
        tempitemManager.RemoveAt(ActNum);
        itemManager = tempitemManager.ToArray();
    }
    public void DeleteItem()
    {
        Debug.Log("재생2");
        itemManager = tempitemManager.ToArray();
        CurrentItemManager = new List<ItemName>(tempitemManager);
    }
    public void ShowItem()
    {
        Debug.Log("재생3");
        switch (PlayerTurn)
        {
            case 1: tempitemManager = new List<ItemName>(OriginalitemManager);break;
            case 2: tempitemManager = new List<ItemName>(CurrentItemManager); break;
            case 3: tempitemManager = new List<ItemName>(P3ItemManager); break;
        }
        itemManager = tempitemManager.ToArray();
    }
    public void UpdateItem()
    {
        switch (PlayerTurn)
        {
            case 1: CurrentItemManager = new List<ItemName>(tempitemManager);break;
            case 2: P3ItemManager = new List<ItemName>(tempitemManager); break;
        }
    }
}
