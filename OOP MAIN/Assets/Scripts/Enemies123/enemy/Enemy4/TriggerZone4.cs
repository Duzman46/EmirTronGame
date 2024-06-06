using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone4 : MonoBehaviour
{
    private Enemy4_Move enemy;
    void Start()
    {
        enemy = GetComponentInParent<Enemy4_Move>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            enemy.target = collision.transform;
            enemy.inFocus = true;
            enemy.actionZone.SetActive(true);
            enemy.an.SetBool("isShot",false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            enemy.SelectTarget();
            enemy.inFocus = false;
            enemy.moveSpeed = 1f;
            enemy.an.SetBool("walk",true);
        } 
    }
}
