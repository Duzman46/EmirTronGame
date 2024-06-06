using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enem2_Stats : EnemyHealth
{ 
    private Animator an;
    public bool isStunned;
    private Enemy2 enemy2;
    private float timer2=0.8f;
    public float maxHealth = 100f;
    public float crtHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        if (collision.CompareTag("sword"))
            TakeDamage(player.tempDamage);
    }

    void Start()
    {
        an = GetComponent<Animator>();
        enemy2=GetComponentInParent<Enemy2>();
        currentHealth = maxHealth;
        crtHealth = maxHealth;
    }

    

    public override void TakeDamage(float damage)
    {
        if(!isStunned)
        {
            if(crtHealth >0)
            {
                crtHealth -= damage;
                an.SetBool("CanWalk", false);
                an.SetBool("Attack", false);
                an.SetBool("Hit",true);
                enemy2.moveSpeed = 0f;
                StartCoroutine(StunnedCoroutine(timer2));
            }
            else
            {
                crtHealth =0;
                an.SetBool("CanWalk", false);
                an.SetBool("Attack", false);
                an.SetBool("Death",true);
                enemy2.moveSpeed=0f; 
                StartCoroutine(DeadCoroutine(0.5f));
            }


        }

    }
    IEnumerator StunnedCoroutine(float timer)
    {
        yield return new WaitForSeconds(timer);
        isStunned = false;
        an.SetBool("Hit",false);
        enemy2.moveSpeed = 2f;
        an.SetBool("CanWalk",true);
    }
    IEnumerator DeadCoroutine(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
