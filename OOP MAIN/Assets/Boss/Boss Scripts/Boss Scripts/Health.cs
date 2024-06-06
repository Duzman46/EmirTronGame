using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 400;
    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            CoinManager.instance.Money(100);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("arrow"))
        {
            TakeDamage(PlayerController.Instance.damage);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("sword")||collision.CompareTag("Boss"))
        {
            TakeDamage(PlayerController.Instance.tempDamage);
        }
    }
}
