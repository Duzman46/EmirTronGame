using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public GameObject[] slots;
    //public GameObject[] backpack;
    bool isInstantiated;

    TextMeshProUGUI amountText;

    public Dictionary<string,int> InventoryItems = new Dictionary<string,int>();

    public ItemList itemlist;

    void Start()
    {
        if(itemlist!= null)
        {
            DataToInventory();
        }
    }

    void Update()
    {
        
    }
    public void CheckSlotsAvailableity(GameObject itemToAdd,string itemName,int itemAmount)
    {
        isInstantiated = false;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount>0)
            {
                slots[i].GetComponent<Slots>().isUsed=true;
            }
            else if(!isInstantiated && !slots[i].GetComponent<Slots>().isUsed)
            {
                if (!InventoryItems.ContainsKey(itemName))
                {
                    GameObject item = Instantiate(itemToAdd, slots[i].transform.position, Quaternion.identity);
                    item.transform.SetParent(slots[i].transform, false);
                    item.transform.localPosition = new Vector3(0, 0, 0);
                    item.name = item.name.Replace("(Clone)", "");
                    isInstantiated = true;
                    slots[i].GetComponent<Slots>().isUsed = true;
                    InventoryItems.Add(itemName, itemAmount);
                    amountText = slots[i].GetComponentInChildren<TextMeshProUGUI>();
                    amountText.text = itemAmount.ToString();
                    break;
                }
                else
                {
                    for (int j = 0; j < slots.Length; j++)
                    {
                        if (slots[j].transform.GetChild(0).gameObject.name == itemName)
                        {
                            InventoryItems[itemName] += itemAmount;
                            amountText = slots[j].GetComponentInChildren<TextMeshProUGUI>();
                            amountText.text = InventoryItems[itemName].ToString();
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }

    public void UseInventoryItems(string itemName)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].GetComponent<Slots>().isUsed)
            {
                continue;
            }
            if (slots[i].transform.GetChild(0).gameObject.name == itemName)
            {
                InventoryItems[itemName]--;
                amountText = slots[i].GetComponentInChildren<TextMeshProUGUI>();
                amountText.text = InventoryItems[itemName].ToString();

                if (InventoryItems[itemName]<=0)
                {
                    Destroy(slots[i].transform.GetChild(0).gameObject);
                    slots[i].GetComponent<Slots>().isUsed = false;
                    InventoryItems.Remove(itemName);
                    ReorganizedInv();
                }
                break;
            }
        }
    }
    public void ReorganizedInv()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].GetComponent<Slots>().isUsed)
            {
                for (int j = i+1; j < slots.Length; j++)
                {
                    if (slots[j].GetComponent<Slots>().isUsed)
                    {
                        Transform itemMove = slots[j].transform.GetChild(0).transform;
                        itemMove.transform.SetParent(slots[i].transform,false);
                        itemMove.transform.localPosition = new Vector3(0, 0, 0);
                        slots[i].GetComponent<Slots>().isUsed = true;
                        slots[j].GetComponent<Slots>().isUsed = false;
                        break;
                    }
                }
            }
        }
    }

    public void DataToInventory()
    {
        for (int i = 0; i < GameData.instance.saveData.addID.Count; i++)
        {
            for (int j = 0; j < itemlist.items.Count; j++)
            {
                if (itemlist.items[j].ID == GameData.instance.saveData.addID[i])
                {
                    CheckSlotsAvailableity(itemlist.items[j].gameObject, GameData.instance.saveData.inventoryItemsName[i], GameData.instance.saveData.inventoryItemsAmount[i]);
                }
            }
        }
    }

    public void InvertoryToData()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].GetComponent<Slots>().isUsed)
            {
                if (!GameData.instance.saveData.addID.Contains(slots[i].GetComponentInChildren<ItemUse>().ID))
                {
                    GameData.instance.saveData.addID.Add(slots[i].GetComponentInChildren<ItemUse>().ID);
                    GameData.instance.saveData.inventoryItemsName.Add(slots[i].GetComponentInChildren<ItemUse>().name);
                    GameData.instance.saveData.inventoryItemsAmount.Add(InventoryItems[slots[i].GetComponentInChildren<ItemUse>().name]);
                }
            }
        }
    }
}
