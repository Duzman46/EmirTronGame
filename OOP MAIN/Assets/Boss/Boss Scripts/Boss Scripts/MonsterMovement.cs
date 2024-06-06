using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public Transform[] patrolPoints; // Devriye noktalar�
    public float moveSpeed; // Hareket h�z�
    public int patrolDestination; // Mevcut devriye hedefi
    public Transform playerTransform; // Oyuncunun Transform bile�eni
    public bool isChasing; // Takip edip etmedi�ini belirler
    public float chaseDistance; // Takip etme mesafesi
    Rigidbody2D rb;
    bool faceRight=true;

    // Update is called once per frame
    void Update()
    {
        // Oyuncu ile canavar aras�ndaki mesafeyi kontrol et
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Oyuncuyu belirli bir mesafede g�r�rse takibe ba�la
        if (distanceToPlayer < chaseDistance)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            // Takip etme hareketi
            if (transform.position.x > playerTransform.position.x)
            {
                transform.localScale = new Vector3(-0.25f, 0.25f, 1.0f);
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
            else if (transform.position.x < playerTransform.position.x)
            {
                transform.localScale = new Vector3(0.25f, 0.25f, 1.0f);
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (faceRight)
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                transform.localScale = new Vector3(-0.25f, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                transform.localScale = new Vector3(0.25f, transform.localScale.y, transform.localScale.z);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("point"))
        {
            faceRight = !faceRight;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
