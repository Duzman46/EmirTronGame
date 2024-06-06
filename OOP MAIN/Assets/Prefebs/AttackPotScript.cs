using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPotScript : MonoBehaviour
{
    GameManagerTwo gameManager;
    Inventory inventory;

    public GameObject itemToAdd;
    public int itemAmount;

    float StrPot = 20;

    bool isStrPot = false;

    void Start()
    {
        gameManager = GameManagerTwo.Instance;
        inventory = gameManager.GetComponent<Inventory>();
    }
    public void UseStr()
    {
        if (!isStrPot)
        {
            PlayerController.Instance.damage += StrPot;
            isStrPot = true;
            StartCoroutine(Count());
        }
    }

    IEnumerator Count()
    {
        yield return new WaitForSeconds(40);
        PlayerController.Instance.damage -= StrPot;
        isStrPot = false;
    }
}
