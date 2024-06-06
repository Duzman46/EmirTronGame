using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiRocket : MonoBehaviour
{
    public Transform playerTransform; 
    public GameObject rocketPrefab;   
    public float chaseDistance = 10f; 
    public float rocketSpeed = 5f;     
    public float rocketInterval = 2f;  
    public float rocketOffset = 1.5f;  

    private float nextRocketTime = 0f;

    public static EnemyAiRocket instance;

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < chaseDistance)
        {
            if (Time.time >= nextRocketTime)
            {
                FireRocket();
                nextRocketTime = Time.time + rocketInterval;
            }
        }
    }

    void FireRocket()
    {
        if (rocketPrefab == null)
        {
            Debug.LogError("Rocket Prefab is not assigned!");
            return;
        }

        Vector2 direction = (playerTransform.position - transform.position).normalized;
        Vector2 rocketPosition = (Vector2)transform.position + direction * rocketOffset; 

        GameObject rocket = Instantiate(rocketPrefab, rocketPosition, Quaternion.identity);
        Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = direction * rocketSpeed;
        }

        Rocket rocketScript = rocket.GetComponent<Rocket>();
        if (rocketScript != null)
        {
            rocketScript.ownerTag = gameObject.tag;
        }
    }
}
