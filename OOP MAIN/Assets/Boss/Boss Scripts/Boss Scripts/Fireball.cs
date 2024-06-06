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
        // Oyuncu atanm��sa ate� topunu oyuncuya do�ru hareket ettir
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
            // �arp��ma i�lemleri (�rne�in, hasar verme, yok etme vs.)
            Destroy(gameObject);  // Ate� topunu yok et
        }
    }
}


