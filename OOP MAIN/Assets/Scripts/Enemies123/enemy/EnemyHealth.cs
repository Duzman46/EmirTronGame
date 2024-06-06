using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage);
}

public class EnemyHealth : MonoBehaviour, IDamageable
{
    protected float maxHealth = 100f;
    protected float damage = 20f;
    private float timer=1f;
    [SerializeField]
    protected  float currentHealth;
    [SerializeField]
    private Transform player1;
    private Rigidbody2D rb2;
    private EnemyMovement movement;
    private HitEffect hitEffect;
    private Animator an;
    void Start()
    {
        currentHealth = maxHealth;
        hitEffect = GetComponent<HitEffect>();
        rb2 = GetComponent<Rigidbody2D>();
        movement = GetComponent<EnemyMovement>();
        an = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Stunned();
        if(currentHealth < 0)
        {
            currentHealth =0 ;
            Destroy(gameObject);
        }
    }
    IEnumerator BackTheOriginal()
    {
        yield return new WaitForSeconds(timer);
        GetComponent<SpriteRenderer>().material = hitEffect.original;
        an.SetBool("isStunned", false);
        an.SetBool("idle", true);
        movement.speed = 30f;
    }
    private void Stunned()
    {
        movement.speed =0f;
        an.SetBool("idle", false);
        an.SetBool("isStunned", true);
        GetComponent<SpriteRenderer>().material = hitEffect.white;
        StartCoroutine(BackTheOriginal());
    }
}
