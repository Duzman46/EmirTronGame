using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNpc : MonoBehaviour
{
    public GameObject[] itemInStore;
    Inventory inventory;
    public bool sellItems;
    public GameObject shopPanel;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        SetUpShop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpShop()
    {
        for (int i = 0; i < itemInStore.Length; i++)
        {
            GameObject itemToSell = Instantiate(itemInStore[i], inventory.slots[i].transform.position, Quaternion.identity);
            itemToSell.transform.SetParent(inventory.slots[i].transform, false);
            itemToSell.transform.localPosition = new Vector3(0,0, 0);
            itemToSell.name = itemToSell.name.Replace("(Clone)", "");
        }
    }

    public void IsSellingItems()
    {
        sellItems = !sellItems;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            shopPanel.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            shopPanel.SetActive(false);
        }
    }
}
