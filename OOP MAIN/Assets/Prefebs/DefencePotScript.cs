using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DefencePotScript : MonoBehaviour
{
    GameManagerTwo gameManager;
    Inventory inventory;

    public GameObject itemToAdd;
    public int itemAmount;

    float defensePot = 20;

    bool isDefPot = false;

    void Start()
    {
        gameManager = GameManagerTwo.Instance;
        inventory = gameManager.GetComponent<Inventory>();
    }


    void Update()
    {

    }

    public void useDefPot()
    {
        if (isDefPot)
        {
            PlayerHealth.instance.defense += defensePot;
            isDefPot = true;
            StartCoroutine(Count());
        }
    }

    IEnumerator Count()
    {
        yield return new WaitForSeconds(40);
        PlayerHealth.instance.defense -= defensePot;
        isDefPot= false;
    }
}
