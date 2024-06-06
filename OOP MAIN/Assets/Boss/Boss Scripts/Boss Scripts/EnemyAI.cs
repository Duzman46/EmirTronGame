using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform playerTransform;  // Ana karakterin Transform bile�eni
    public GameObject fireballPrefab;  // Ate� topu prefab'�
    public float chaseDistance = 10f;  // Takip mesafesi
    public float fireballSpeed = 5f;   // Ate� topu h�z�
    public float fireballInterval = 2f; // Ate� topu f�rlatma aral���
    public float fireballOffset = 1.0f; // Ate� topu do�ma mesafesi

    private float nextFireballTime = 0f;

    void Update()
    {
        // Ana karakter ile d��man aras�ndaki mesafeyi kontrol et
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < chaseDistance)
        {
            // Ate� topu f�rlatma zaman� geldiyse
            if (Time.time >= nextFireballTime)
            {
                FireFireball();
                nextFireballTime = Time.time + fireballInterval;
            }
        }
    }

    void FireFireball()
    {
        // Ate� topunu olu�tur
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);

        // Ate� topunun y�n�n� belirle ve h�z�n� ayarla
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * fireballSpeed;
        }

        // Ate� topunun sahibini belirle
        Fireball fireballScript = fireball.GetComponent<Fireball>();
        if (fireballScript != null)
        {
            fireballScript.SetOwnerTag(gameObject);
        }
    }
}
