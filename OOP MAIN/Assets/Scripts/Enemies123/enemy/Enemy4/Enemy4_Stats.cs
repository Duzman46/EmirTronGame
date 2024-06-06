using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4_Stats : EnemyHealth
{
    public bool isStunned ;
    public float stunnedTime = 3;
    private Rigidbody2D rb;
    private Animator an;
    private Enemy4_Move enemy;
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        enemy = GetComponent<Enemy4_Move>();

    }

    // Update is called once per frame
    void Update()
    {
        stunnedTime -=Time.deltaTime;
    }
        
    public override void TakeDamage(float damage)
    {
        if(stunnedTime <=0)
        {
            isStunned = true;
            currentHealth -= damage;
            an.SetBool("isStunned",true);
            stunnedTime = 3f;
            StartCoroutine(StunnedTimer());
        }
        if(currentHealth < 0)
        {
            currentHealth =0 ;
            enemy.moveSpeed = 0f;
            an.SetBool("isStunned",false);
            an.SetBool("idle",false);
            an.SetBool("dead",true);
            StartCoroutine(Dead());
        }
        
    }
    IEnumerator StunnedTimer()
    {
        yield return new WaitForSeconds(2.3f);
        isStunned = false;
        an.SetBool("isStunned",false);
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.5f);
        Die();

    }
}

