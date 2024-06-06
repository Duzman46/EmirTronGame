using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    public int ID;
    public float healthToGive;
    public float manaToGive;
    public float damageToGive;
    public float speedToGive;
    public float defenseToGive;

    bool isStr = false, isSpd = false, isDfs = false;

    public void Use()
    {
        if (damageToGive > 0 && !isStr)
            StartCoroutine(StrPot());
        if (defenseToGive > 0 && !isDfs)
            StartCoroutine(DfsPot());
        if (speedToGive > 0 && !isSpd) 
            StartCoroutine(SpdPot());
        if (healthToGive > 0)
        {
            if (PlayerHealth.instance.currentHealth != PlayerHealth.instance.maxHealth)
                PlayerHealth.instance.currentHealth += healthToGive;

            if(PlayerHealth.instance.currentHealth > PlayerHealth.instance.maxHealth) PlayerHealth.instance.currentHealth = PlayerHealth.instance.maxHealth;
        }

    }

    IEnumerator StrPot()
    {
        PlayerController.Instance.damage += damageToGive;
        isStr = true;
        yield return new WaitForSeconds(40);
        isStr = false;
        PlayerController.Instance.damage -= damageToGive;
    }

    IEnumerator SpdPot()
    {
        PlayerController.Instance.speed += speedToGive;
        isSpd = true;
        yield return new WaitForSeconds(40);
        isSpd = false;
        PlayerController.Instance.speed -= speedToGive;
    }

    IEnumerator DfsPot()
    {
        PlayerHealth.instance.defense += defenseToGive;
        isDfs = true;
        yield return new WaitForSeconds(40);
        isDfs = false;
        PlayerHealth.instance.defense -= defenseToGive;
    }
}
