using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveRoom : MonoBehaviour
{
    public GameObject saveText;
    private void Start()
    {
        if (PlayerPrefs.HasKey("x") && PlayerPrefs.HasKey("y") && PlayerPrefs.HasKey("z"))
        {
            float x = PlayerPrefs.GetFloat("x");
            float y = PlayerPrefs.GetFloat("y");
            float z = PlayerPrefs.GetFloat("z");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = new Vector3(x, y, z);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("x", collision.transform.position.x);
            PlayerPrefs.SetFloat("y", collision.transform.position.y);
            PlayerPrefs.SetFloat("z", collision.transform.position.z);
            saveText.SetActive(true);
            StartCoroutine(CloseText());
            Experience.instance.DataSave();
        }
    }
    IEnumerator CloseText()
    {
        yield return new WaitForSeconds(2);
        saveText.SetActive(false);
        transform.GetComponent<BoxCollider2D>().enabled = false;
    }
}