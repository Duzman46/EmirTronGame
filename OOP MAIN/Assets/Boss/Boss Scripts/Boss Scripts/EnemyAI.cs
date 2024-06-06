using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform playerTransform;  // Ana karakterin Transform bileþeni
    public GameObject fireballPrefab;  // Ateþ topu prefab'ý
    public float chaseDistance = 10f;  // Takip mesafesi
    public float fireballSpeed = 5f;   // Ateþ topu hýzý
    public float fireballInterval = 2f; // Ateþ topu fýrlatma aralýðý
    public float fireballOffset = 1.0f; // Ateþ topu doðma mesafesi

    private float nextFireballTime = 0f;

    void Update()
    {
        // Ana karakter ile düþman arasýndaki mesafeyi kontrol et
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < chaseDistance)
        {
            // Ateþ topu fýrlatma zamaný geldiyse
            if (Time.time >= nextFireballTime)
            {
                FireFireball();
                nextFireballTime = Time.time + fireballInterval;
            }
        }
    }

    void FireFireball()
    {
        // Ateþ topunu oluþtur
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);

        // Ateþ topunun yönünü belirle ve hýzýný ayarla
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * fireballSpeed;
        }

        // Ateþ topunun sahibini belirle
        Fireball fireballScript = fireball.GetComponent<Fireball>();
        if (fireballScript != null)
        {
            fireballScript.SetOwnerTag(gameObject);
        }
    }
}
