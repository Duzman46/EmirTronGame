using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    int count = 1;
    public int strengthPotCount = 2, speedPotCount = 2, jumpPotCount = 2, defencePotCount = 2, smallHealthPotCount = 2, bigHealthPotCount = 2;
    float strengthPot = 25f;
    float speedPot = 2f;
    float jumpPot = 3f;
    float defencePot = 30f;
    float smallHealthPot = 25f;
    float bigHealthPot = 50f;
    public float shieldProtection = 20f;
    //float nextStr=0f, nextSpd=0f, nextJmp=0f, nextDfc=0f;
    bool isStr = false, isSpd = false, isJmp = false, isDfc = false;
    //bool firstWeapon = true,secondWeapon=false,thirdWeapon=false;
    
    //bool firstArmour=true,secondArmour=false,thirdArmour=false;
    //bool firstBoot=true,secondBoot=false,thirdBoot=false;
    //bool firstGlove=true,secondGlove=false,thirdGlove=false;


    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) Debug.Log(count);
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            count++;
            if (count == 7) count = 1;
        }
        if (Input.GetKeyDown(KeyCode.Q))
            pot();
    }

    /*void equipment()
    {
        PlayerControl player = GameObject.Find("Player").GetComponent<PlayerControl>();
        PlayerHealth playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        
        if (firstWeapon && !secondWeapon && !thirdWeapon)
        {
            player.tempDamage += 10;
            if (isStr) player.tempDamage += 20;
        }else if(!firstWeapon && secondWeapon && !thirdWeapon)
        {
            player.tempDamage += 20;
            if (isStr) player.tempDamage += 30;
        }
        else if (!firstWeapon && !secondWeapon && thirdWeapon)
        {
            player.tempDamage += 30;
            if (isStr) player.tempDamage += 40;
        }
        if (firstWeapon && !secondWeapon && !thirdWeapon) ;

    }
    */


    /*void pot()
    {
        PlayerControl player = GameObject.Find("Player").GetComponent<PlayerControl>();
        PlayerHealth playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        if (Time.time > nextStr&&strengthPotCount>0&&count==1)
        {
            player.damage += strengthPot;
            strengthPotCount -= 1;
            nextStr = Time.time + 40f; 
        }
    }*/



    IEnumerator strenthPotM()
    {

        PlayerController.Instance.damage += strengthPot;
        Debug.Log("Guc calisti");
        strengthPotCount -= 1;
        isStr = true;
        yield return new WaitForSeconds(40);
        isStr = false;
        PlayerController.Instance.damage -= strengthPot;
    }

    IEnumerator speedPotM()
    {
        PlayerController.Instance.speed += speedPot;
        Debug.Log("Hiz calisti");
        speedPotCount -= 1;
        isSpd = true;
        yield return new WaitForSeconds(40f);
        isSpd = false;
        PlayerController.Instance.speed -= speedPot;
    }

    IEnumerator jumpPotM()
    {
        PlayerController.Instance.jumpPower += jumpPot;
        Debug.Log("Ziplama calisti");
        jumpPotCount -= 1;
        isJmp = true;
        yield return new WaitForSeconds(40f);
        isJmp = false;
        PlayerController.Instance.jumpPower -= jumpPot;
    }

    IEnumerator defencePotM()
    {
        PlayerHealth.instance.defense = defencePot;
        Debug.Log("Defans calisti");
        defencePotCount -= 1;
        isDfc = true;
        yield return new WaitForSeconds(40f);
        isDfc = false;
        PlayerHealth.instance.defense = 1;
    }

    void pot()
    {
        PlayerHealth playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        if (count == 1 && strengthPotCount > 0&&!isStr) StartCoroutine(strenthPotM());
        if (count == 2 && speedPotCount > 0&&!isSpd) StartCoroutine(speedPotM());
        if (count == 3 && jumpPotCount > 0&&!isJmp) StartCoroutine(jumpPotM());
        if (count == 4 && defencePotCount > 0&&!isDfc) StartCoroutine(defencePotM());
        if (count == 5 && smallHealthPotCount > 0)
        {
            if (playerHealth.currentHealth == playerHealth.maxHealth)
                return;
            playerHealth.currentHealth += smallHealthPot;
            if (playerHealth.currentHealth > playerHealth.maxHealth) playerHealth.currentHealth = playerHealth.maxHealth;
            smallHealthPotCount -= 1;
            Debug.Log("Kucuk can calisti");
        }
        if (count == 6 && bigHealthPotCount > 0)
        {
            if (playerHealth.currentHealth == playerHealth.maxHealth)
                return;
            playerHealth.currentHealth += bigHealthPot;
            if (playerHealth.currentHealth > playerHealth.maxHealth) playerHealth.currentHealth = playerHealth.maxHealth;
            bigHealthPotCount -= 1;
            Debug.Log("Buyuk can calisti");
        }
    }
}