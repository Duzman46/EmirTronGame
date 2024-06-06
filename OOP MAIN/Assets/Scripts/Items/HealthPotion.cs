using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public float healthToGive;

    GameManagerTwo gameManager;
    Inventory inventory;

    public GameObject itemToAdd;
    public int itemAmount;

    void Start()
    {
        gameManager = GameManagerTwo.Instance;
        inventory = gameManager.GetComponent<Inventory>(); 
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TriggerZone"))
        {
            inventory.CheckSlotsAvailableity(itemToAdd, itemToAdd.name, itemAmount);
            //collision.GetComponent<PlayerHealth>().currentHealth += healthToGive;
            Destroy(gameObject);
        }
    }
}