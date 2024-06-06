using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotScript : MonoBehaviour
{
    float speedPot = 2f;
    bool isUsingSpd = false;

    GameManagerTwo gameManager;
    Inventory inventory;

    public GameObject itemToAdd;
    public int itemAmount;

    void Start()
    {
        gameManager = GameManagerTwo.Instance;
        inventory = gameManager.GetComponent<Inventory>();

    }

    public void useSpeed()
    {
        if (!isUsingSpd)
        {
            isUsingSpd = true;
            PlayerController.Instance.speed += speedPot;
            StartCoroutine(Count());
        }
    }

    IEnumerator Count()
    {
        yield return new WaitForSeconds(40);
        isUsingSpd = true;
        PlayerController.Instance.speed -= speedPot;
    }
}
