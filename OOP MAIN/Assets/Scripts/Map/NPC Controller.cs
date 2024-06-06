using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public static NPCController instance;

    public string[] dialogue;
    public string nameOfnpc;

    private void Awake()
    {
        instance = this;

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DialogueSystem.instance.AddDialogue(dialogue,nameOfnpc);
        }
    }
}
