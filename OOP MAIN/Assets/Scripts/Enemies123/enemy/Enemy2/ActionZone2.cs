using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionZone : MonoBehaviour
{
    private bool inRange;
    private Animator an;
    private Enemy2 enemy2;
    void Start()
    {
        an = GetComponentInParent<Animator>();
        enemy2 = GetComponentInParent<Enemy2>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange && !an.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            enemy2.Flip();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            inRange = true;
            enemy2.target = collision.transform;
            enemy2.inRange = true;
            enemy2.actionZone.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            inRange = false;
            gameObject.SetActive(false);
            enemy2.triggerZone.SetActive(true);
            enemy2.inRange = false;
            enemy2.SelectTarget();
        }
    }
}
