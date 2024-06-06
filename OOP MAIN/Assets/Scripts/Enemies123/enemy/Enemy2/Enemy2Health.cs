using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth < 0)
        {
            Destroy(gameObject);
        }
    }
    
    void Update()
    {
        
    }
}
