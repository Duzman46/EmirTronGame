using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float explosionRadius = 2f;   
    public float explosionForce = 700f;  
    public int damage = 40;             
    public string ownerTag;          
    

    // Patlama efekti
    public GameObject explosionEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")|| collision.CompareTag("Ground"))
        {
            Explode();
            Destroy(gameObject); 
        }
    }

    void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D nearbyObject in colliders)
        {
            Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            if (nearbyObject.CompareTag("Player"))
            {
                PlayerHealth playerHealth = nearbyObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }
            }
        }
    }
}

public static class Rigidbody2DExtension
{
    public static void AddExplosionForce(this Rigidbody2D rb, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        var explosionDir = rb.position - (Vector2)explosionPosition;
        float explosionDistance = explosionDir.magnitude;
        if (explosionDistance <= explosionRadius)
        {
            float force = Mathf.Lerp(0, explosionForce, (explosionRadius - explosionDistance) / explosionRadius);
            rb.AddForce(explosionDir.normalized * force);
        }
    }
}
