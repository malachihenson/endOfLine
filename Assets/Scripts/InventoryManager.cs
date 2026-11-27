using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject invContainer;
    [SerializeField] private GameObject loadoutContainer;
    [SerializeField] private TextMeshProUGUI invList;
    [SerializeField] private TextMeshProUGUI invCountText;
    [SerializeField] private TextMeshProUGUI itemOptions;
    [SerializeField] private TextMeshProUGUI itemHeader;
    [SerializeField] private TextMeshProUGUI itemDesc;
    [SerializeField] private GameObject invSelector;
    [SerializeField] private GameObject invChoice;
    [SerializeField] private GameObject loadSelector;
    [SerializeField] private TextAsset[] langFiles;
    [SerializeField] private SettingsManager sm;
    [SerializeField] private Sprite[] loadoutIcons;
    [SerializeField] private Image[] loadoutIconSlots;
    private bool invOpen = false;
    private List<int> items = new List<int>();
    private Vector2[] gridPositions = {new Vector2(-25, 25), new Vector2(25, 25), new Vector2(-25, -25), new Vector2(-25, 25)};
    private InventoryItems allItems;
    private int invCount;
    private string listToUpdate;
    private int selectorPos;
    private int subSelectorPos;
    private int loadoutPos;
    private int selectorState;
    public int slot1;
    public int slot2;
    public int slot3;
    public int slot4;
    private int pendingSwap;
    private int slotsTaken;
    private int valueToLoad;
    private int slotToSwitch;
    private int prevItemType;
    void Start()
    {
        for(int i=0; i<16; i++)
        {
            items.Add(UnityEngine.Random.Range(0, 2));
        }
        invContainer.SetActive(false);
        loadoutContainer.SetActive(false);
        //eventually pull items + loadout from save here
        allItems = JsonUtility.FromJson<InventoryItems>(langFiles[sm.language].text);
        slot1 = slot2 = slot3 = slot4 = -1;
        slotsTaken = 0;
    }
    void Update()
    {
        
    }
    public void ToggleInv()
    {
        //eventually add a sliding animation here
        if(!invOpen)
        {
            invContainer.SetActive(true);
            loadoutContainer.SetActive(true);
            invChoice.SetActive(false);
            loadSelector.SetActive(false);
            itemHeader.text = "";
            itemDesc.text = "";
            invOpen = true;
            UpdateList();
            if(invCount > 0)
            {
                invSelector.SetActive(true);
                selectorPos = 0;
                selectorState = 0;
                invSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-11, 78);
            } else
            {
                invSelector.SetActive(false);
            }
        } else
        {
            invContainer.SetActive(false);
            loadoutContainer.SetActive(false);
            invOpen = false;
        }
    }
    public void SelectItem()
    {
        switch(selectorState)
        {
            case 0:
                selectorState = 1;
                invChoice.SetActive(true);
                invChoice.GetComponent<RectTransform>().anchoredPosition = new Vector2(90, 78 - (12.5f * selectorPos));
                subSelectorPos = 0;
                invSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(90, 95 - (12.5f * selectorPos));
                switch(allItems.items[items[selectorPos]].type)
                {
                    case 0:
                        itemOptions.text = "Swap\nThrow Away\nCancel";
                        break;
                    case 1:
                        itemOptions.text = "View\nThrow Away\nCancel";
                        break;
                }
                break;
            case 1:
                if(subSelectorPos == 1)
                {
                    items.RemoveAt(selectorPos);
                    if(selectorPos == invCount - 1)
                    {
                        selectorPos--;
                    }
                    UpdateList();
                    
                } else if(subSelectorPos == 0)
                {
                    prevItemType = allItems.items[items[selectorPos]].type;
                    if(prevItemType == 0)
                    {
                        if(slotsTaken < 4)
                        {
                            switch(slotsTaken)
                            {
                                case 0:
                                    slot1 = items[selectorPos];
                                    break;
                                case 1:
                                    slot2 = items[selectorPos];
                                    break;
                                case 2:
                                    slot3 = items[selectorPos];
                                    break;
                                case 3:
                                    slot4 = items[selectorPos];
                                    break;
                            }
                            slotToSwitch = slotsTaken;
                            items.RemoveAt(selectorPos);
                            slotsTaken++;
                            UpdateSlots();
                            LoadDescription();
                            UpdateList();
                            selectorState = 2;
                            loadoutPos = slotToSwitch;
                            loadSelector.GetComponent<RectTransform>().anchoredPosition = gridPositions[slotToSwitch];
                        } else
                        {
                            pendingSwap = items[selectorPos];
                            items.RemoveAt(selectorPos);
                            selectorState = 3;
                            itemHeader.text = "";
                            itemDesc.text = "Select an item to replace.";
                            loadoutPos = 0;
                            loadSelector.GetComponent<RectTransform>().anchoredPosition = gridPositions[0];
                        }
                        invSelector.SetActive(false);
                        loadSelector.SetActive(true);
                    } else
                    {
                        //view item
                    }
                }
                if(subSelectorPos == 1 || subSelectorPos == 2)
                {
                    selectorState = 0;
                }
                invChoice.SetActive(false);
                invSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-11, 78 - (12.5f * selectorPos));
                break;
            case 2:
                if(invCount < 16)
                {
                    switch(loadoutPos)
                    {
                        case 0:
                            items.Add(slot1);
                            slot1 = slot2;
                            slot2 = slot3;
                            slot3 = slot4;
                            slot4 = -1;
                            break;
                        case 1:
                            items.Add(slot2);
                            slot2 = slot3;
                            slot3 = slot4;
                            slot4 = -1;
                            break;
                        case 2:
                            items.Add(slot3);
                            slot3 = slot4;
                            slot4 = -1;
                            break;
                        case 3:
                            items.Add(slot4);
                            slot4 = -1;
                            break;
                    }
                    slotsTaken--;
                    UpdateList();
                    UpdateSlots();
                    selectorState = 0;
                    loadSelector.SetActive(false);
                    invSelector.SetActive(true);
                    selectorPos = invCount - 1;
                    invSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-11, 78 - (selectorPos * 12.5f));
                } else
                {
                    //add text saying inventory full eventually
                }
                break;
            case 3:
                switch(loadoutPos)
                {
                    case 0:
                        items.Add(slot1);
                        slot1 = pendingSwap;
                        break;
                    case 1:
                        items.Add(slot2);
                        slot2 = pendingSwap;
                        break;
                    case 2:
                        items.Add(slot3);
                        slot3 = pendingSwap;
                        break;
                    case 3:
                        items.Add(slot4);
                        slot4 = pendingSwap;
                        break;
                }
                selectorState = 2;
                UpdateSlots();
                LoadDescription();
                UpdateList();
                break;
        }
    }
    public void ChangeSelection(int dir)
    {
        switch(selectorState)
        {
            case 0:
                if(dir == 1)
                {
                    if(selectorPos == 0)
                    {
                        invSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-11, 78 - (12.5f * (invCount - 1)));
                        selectorPos = invCount - 1;
                    } else
                    {
                        invSelector.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 12.5f);
                        selectorPos--;
                    }
                } else if(dir == 0)
                {
                    if(selectorPos == invCount - 1)
                    {
                        invSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-11, 78);
                        selectorPos = 0;
                    } else
                    {
                        invSelector.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, 12.5f);
                        selectorPos++;
                    }
                } else if(dir == 3)
                {
                    selectorState = 2;
                    invSelector.SetActive(false);
                    loadSelector.SetActive(true);
                    loadoutPos = 0;
                    loadSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-25, 25);
                    LoadDescription();
                }
                break;
            case 1:
                if(dir == 1)
                {
                    if(subSelectorPos == 0)
                    {
                        invSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(90, 59 - (12.5f * selectorPos));
                        subSelectorPos = 2;
                    } else
                    {
                        invSelector.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 18);
                        subSelectorPos--;
                    }
                } else if(dir == 0)
                {
                    if(subSelectorPos == 2)
                    {
                        invSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(90, 95 - (12.5f * selectorPos));
                        subSelectorPos = 0;
                    } else
                    {
                        invSelector.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, 18);
                        subSelectorPos++;
                    }
                }
                break;
            case 2:
                switch(loadoutPos)
                {
                    case 0:
                        switch(dir)
                        {
                            case 0:
                            case 1:
                                loadSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-25, -25);
                                loadoutPos = 2;
                                break;
                            case 2:
                                selectorState = 0;
                                loadSelector.SetActive(false);
                                invSelector.SetActive(true);
                                selectorPos = 0;
                                invSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-11, 78);
                                break;
                            case 3:
                                loadSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(25, 25);
                                loadoutPos = 1;
                                break;
                        }
                        break;
                    case 1:
                        switch(dir)
                        {
                            case 0:
                            case 1:
                                loadSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(25, -25);
                                loadoutPos = 3;
                                break;
                            case 2:
                            case 3:
                                loadSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-25, 25);
                                loadoutPos = 0;
                                break;
                        }
                        break;
                    case 2:
                        switch(dir)
                        {
                            case 0:
                            case 1:
                                loadSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-25, 25);
                                loadoutPos = 0;
                                break;
                            case 2:
                                selectorState = 0;
                                loadSelector.SetActive(false);
                                invSelector.SetActive(true);
                                selectorPos = 0;
                                invSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-11, 78);
                                break;
                            case 3:
                                loadSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(25, -25);
                                loadoutPos = 3;
                                break;
                        }
                        break;
                    case 3:
                        switch(dir)
                        {
                            case 0:
                            case 1:
                                loadSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(25, 25);
                                loadoutPos = 1;
                                break;
                            case 2:
                            case 3:
                                loadSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-25, -25);
                                loadoutPos = 2;
                                break;
                        }
                        break;
                }
                LoadDescription();
                break;
            case 3:
                switch(loadoutPos)
                {
                    case 0:
                        switch(dir)
                        {
                            case 0:
                            case 1:
                                loadSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-25, -25);
                                loadoutPos = 2;
                                break;
                            case 2:
                            case 3:
                                loadSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(25, 25);
                                loadoutPos = 1;
                                break;
                        }
                        break;
                    case 1:
                        switch(dir)
                        {
                            case 0:
                            case 1:
                                loadSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(25, -25);
                                loadoutPos = 3;
                                break;
                            case 2:
                            case 3:
                                loadSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-25, 25);
                                loadoutPos = 0;
                                break;
                        }
                        break;
                    case 2:
                        switch(dir)
                        {
                            case 0:
                            case 1:
                                loadSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-25, 25);
                                loadoutPos = 0;
                                break;
                            case 2:
                            case 3:
                                loadSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(25, -25);
                                loadoutPos = 3;
                                break;
                        }
                        break;
                    case 3:
                        switch(dir)
                        {
                            case 0:
                            case 1:
                                loadSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(25, 25);
                                loadoutPos = 1;
                                break;
                            case 2:
                            case 3:
                                loadSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-25, -25);
                                loadoutPos = 2;
                                break;
                        }
                        break;
                }
                break;
        }
    }
    private void UpdateList()
    {
        invCount = items.Count;
        invCountText.text = invCount.ToString() + "/16";
        listToUpdate = "";
        for(int i=0; i<invCount; i++)
        {
            listToUpdate += allItems.items[items[i]].name + "\n";
        }
        invList.text = listToUpdate;
    }
    private void LoadDescription()
    {
        if(selectorState == 2)
        {
            switch(loadoutPos)
            {
                case 0:
                    valueToLoad = slot1;
                    break;
                case 1:
                    valueToLoad = slot2;
                    break;
                case 2:
                    valueToLoad = slot3;
                    break;
                case 3:
                    valueToLoad = slot4;
                    break;
            }
            if(valueToLoad == -1)
            {
                itemHeader.text = "None";
                itemDesc.text = "Load an item into this slot to see what it does!";
            } else
            {
                itemHeader.text = allItems.items[valueToLoad].name;
                itemDesc.text = allItems.items[valueToLoad].description;
            }
        } else
        {
            itemHeader.text = "";
            itemDesc.text = "";
        }
    }
    private void UpdateSlots()
    {
        if(slot1 == -1)
        {
            loadoutIconSlots[0].sprite = loadoutIcons[0];
        } else
        {
            loadoutIconSlots[0].sprite = loadoutIcons[slot1 + 1];
        }
        if(slot2 == -1)
        {
            loadoutIconSlots[1].sprite = loadoutIcons[0];
        } else
        {
            loadoutIconSlots[1].sprite = loadoutIcons[slot2 + 1];
        }
        if(slot3 == -1)
        {
            loadoutIconSlots[2].sprite = loadoutIcons[0];
        } else
        {
            loadoutIconSlots[2].sprite = loadoutIcons[slot2 + 1];
        }
        if(slot4 == -1)
        {
            loadoutIconSlots[3].sprite = loadoutIcons[0];
        } else
        {
            loadoutIconSlots[3].sprite = loadoutIcons[slot4 + 1];
        }
    }
}
[System.Serializable]
public class Item
{
    public string name;
    public string description;
    /*type reference
    0: equipable
    1: viewable
    2: nothing*/
    public int type;
    public int id;
}
[System.Serializable]
public class InventoryItems
{
    public Item[] items;
}