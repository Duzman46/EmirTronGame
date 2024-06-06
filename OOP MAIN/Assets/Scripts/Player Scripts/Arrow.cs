using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{
    public float damage;
    Rigidbody2D rb;
    public Vector2 direction;
    void Start()
    {
        Vector2 vector2 = new Vector2(20f,20f);
        direction = PlayerController.Instance.shootDirection;
        //destroyTime();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction*vector2;
    }

    void Update()
    {
        destroyTime();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyStats>().takeDamage(damage);
            Destroy(gameObject);
        }*/
        if (collision.CompareTag("Ground"))
            Destroy(gameObject);
    }

    IEnumerator destroyTime()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
