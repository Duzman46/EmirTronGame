using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone3 : MonoBehaviour
{
    private Enemy3Move enemyMove;
    private Enemy3 enemyTarget;
    private Animator an;
    public bool isTrigger = false;

    private Enemy3 enemy3;
    void Start()
    {
        enemyMove = GetComponentInParent<Enemy3Move>();
        an = GetComponent<Animator>();
        enemy3 = GetComponentInParent<Enemy3>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isTrigger = true;
            if(!enemy3.isStunned)
            {
                an.SetBool("Move", false);
                an.SetBool("MoveFast", true);
                enemyMove.speed = 40F;
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTrigger=false;
        if(collision.CompareTag ("Player"))
        {
            if(!enemy3.isStunned)
            {
            an.SetBool("Move",true);
            an.SetBool("MoveFast", false);
            enemyMove.speed = 20f;
            }
        }
    } 
}
