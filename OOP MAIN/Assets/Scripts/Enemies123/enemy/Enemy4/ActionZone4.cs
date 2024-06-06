using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionZone4 : MonoBehaviour
{
    private bool inRange;
    private Animator an;
    private Enemy4_Move enemy;
    void Start()
    {
        an = GetComponentInParent<Animator>();
        enemy = GetComponentInParent<Enemy4_Move>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange && !an.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            enemy.Flip();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            enemy.shootCoolDown = 7f;
            inRange = true;
            enemy.target = collision.transform;
            enemy.inRange = true;
            enemy.actionZone.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            inRange = false;
            enemy.triggerZone.SetActive(true);
            enemy.inRange = false;
            enemy.an.SetBool("isShot",false);
            enemy.SelectTarget();
        }
    }
}
