using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private GameObject player;
    public float speed = 5f;

    public void SetOwnerTag(GameObject playerObject)
    {
        player = playerObject;
    }

    void Update()
    {
        // Oyuncu atanmýþsa ateþ topunu oyuncuya doðru hareket ettir
        if (player != null)
        {
            Vector2 direction = (transform.position - player.transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != player)
        {
            // Çarpýþma iþlemleri (örneðin, hasar verme, yok etme vs.)
            Destroy(gameObject);  // Ateþ topunu yok et
        }
    }
}


