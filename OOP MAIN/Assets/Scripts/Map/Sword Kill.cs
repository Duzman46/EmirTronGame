using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordKill : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth.instance.Respawn();
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
