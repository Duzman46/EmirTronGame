using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : EnemyHealth
{
    public float timer3=1f;
    public bool isStunned;
    public GameObject triggerZone;
    private Animator an;
    private Rigidbody2D rb3;
    private Enemy3Move enemy3;
    private TriggerZone3 trigger;
    private ExplosionDamage explode;

    void Start()
    {
        currentHealth = maxHealth;
        an = GetComponent<Animator>();
        rb3 = GetComponent<Rigidbody2D>();
        explode = GetComponentInParent<ExplosionDamage>();
        enemy3 = GetComponentInParent<Enemy3Move>();
        trigger = GetComponentInParent<TriggerZone3>();
    }
    void Update()
    {
    }
    public override void TakeDamage(float damage)
    {
        if(!isStunned)
        {
            if(currentHealth>0)
            {
                FirstControl();
                currentHealth -= damage;
                an.SetBool("Stunned",true);
                StartCoroutine(StunnedCoroutine(timer3));
            }
            else
            {   
                FirstControl();
                an.SetBool("Dead",true);
                currentHealth = 0;
                StartCoroutine(DeadCoroutine());
            
            }
        }
    }
    IEnumerator DeadCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        explode.Explode();
        Die();
    }
    IEnumerator StunnedCoroutine(float timer3)
    {
        yield return new WaitForSeconds(timer3);
        isStunned = false;
        an.SetBool("Stunned",false);
        an.SetBool("Move",true);
        enemy3.speed = 20f;
        if(trigger.isTrigger)
        {
            enemy3.speed= 40f;
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    private void FirstControl()
    {
        isStunned = true;
        an.SetBool("Move",false);
        an.SetBool("MoveFast",false);
        an.SetBool("Idle",false);
        enemy3.speed =0f;
    }
}

