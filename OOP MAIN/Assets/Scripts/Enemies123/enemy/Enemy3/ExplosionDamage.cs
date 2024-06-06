using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ExplosionDamage : MonoBehaviour
{
    public float explosionRadius = 5f; 
    public int damage = 50;
    private Animator an;

    public void Explode()
    {
        Vector2 explosionPosition = transform.position;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPosition, explosionRadius);

        foreach (Collider2D hit in colliders)
        {
            PlayerHealth targetHealth = hit.GetComponent<PlayerHealth>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);

            }
        }
    }

    // Patlama yarıçapını çizim için (görsel yardım)
    private  void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}