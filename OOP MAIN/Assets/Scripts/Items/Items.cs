using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public GameObject itemToAdd;
    public int amountToAdd;

    Inventory Inventory;
    GameManagerTwo gameManager;
    void Start()
    {
        gameManager = GameManagerTwo.Instance;
        Inventory = gameManager.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory.CheckSlotsAvailableity(itemToAdd, itemToAdd.name,amountToAdd);
            Destroy(gameObject);
        }
    }
}
