using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpecialElevator : MonoBehaviour
{
    public float speed=0;
    public float realSpeed=4;

    bool knt = true;
    

    Rigidbody2D rb;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("point"))
            speed = -speed;
        if (collision.CompareTag("Player"))
            movementStart();
        if (collision.CompareTag("Special Point"))
        {
            speed = 0f;
            rb.isKinematic = true;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(0, speed);
    }

    void movementStart()
    {
        if (knt)
        {
            rb.isKinematic = false;
            speed = realSpeed;
            knt = false;
        }
    }
}
