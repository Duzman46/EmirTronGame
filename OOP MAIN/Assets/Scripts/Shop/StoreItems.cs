using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreItems : MonoBehaviour
{
    //public string itemName;
    public int itemSellPrice;
    public int itemBuyPrice;

    public GameObject itemToAdd;
    public int amountToAdd;

    public ShopNpc shopNpc;

    TextMeshProUGUI buyPriceText;

    GameManagerTwo gameManager;
    Inventory inventory;
    void Start()
    {
        gameManager = GameManagerTwo.Instance;
        inventory = gameManager.GetComponent<Inventory>();

        buyPriceText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        buyPriceText.text = itemBuyPrice.ToString();
        shopNpc = transform.root.GetComponent<ShopNpc>();
    } 

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    public void BuyItems()
    {
        if (!shopNpc.sellItems)
        {
            if (itemBuyPrice <= CoinManager.instance.bank)
            {
                CoinManager.instance.Money(-itemBuyPrice);
                inventory.CheckSlotsAvailableity(itemToAdd, itemToAdd.name, amountToAdd);
                buyPriceText.text = itemBuyPrice.ToString();
            }
        }
        else if(inventory.InventoryItems.ContainsKey(itemToAdd.name))
        {
            inventory.UseInventoryItems(itemToAdd.name);    
            CoinManager.instance.Money(itemSellPrice);
            buyPriceText.text = itemSellPrice.ToString();
        }

    }
    public void UpdateText()
    {
        if (!shopNpc.sellItems)
        {
            buyPriceText.text = itemBuyPrice.ToString();

        }
        else
        {
            buyPriceText.text = itemSellPrice.ToString();
        }
    }
}
