using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerZone2 : MonoBehaviour
{
    private Enemy2 enemy2;
    // Start is called before the first frame update
    void Start()
    {
        enemy2 = GetComponentInParent<Enemy2>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            enemy2.target = collision.transform;
            enemy2.inRange = true;
            enemy2.actionZone.SetActive(true);
        }
    }
}
