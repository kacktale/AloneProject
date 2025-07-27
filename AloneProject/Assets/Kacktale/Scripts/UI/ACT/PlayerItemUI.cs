using System.Collections;
using System.Collections.Generic;
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

    public GameObject itemPrefab;
    public Transform textArea;

    private TextMeshProUGUI itemText;
    public TextMeshProUGUI itemDescription;

    public PlayerTurnUI playerTurnUI;
    public PlayerTargetUI playerTargetUI;

    public List<TextMeshProUGUI> textColorManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsMyturn)
        {
            ChooseAct(textColorManager);
            SelectItem();
            if (Input.GetKeyUp(KeyCode.X))
            {
                ExitItemList();
                playerTurnUI.SetActSelect();
            }
        }
    }

    public void CreateItemList()
    {
        this.gameObject.SetActive(true);
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
}
