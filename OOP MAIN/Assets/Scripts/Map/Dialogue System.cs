using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;

    public GameObject dialoguePanel;

    public List<string> dialogueLines = new List<string>();

    public string nameOfNPC;

    public Button contButton;

    Text dialogueText, nameText;

    int dialogueIndex;
    private void Awake()
    {
        dialogueText = dialoguePanel.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        nameText = dialoguePanel.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        dialoguePanel.SetActive(false);

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
            instance = this;
    }

    public void AddDialogue(string[] lines, string NPCName)
    {
        dialogueIndex = 0;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);
        this.nameOfNPC = NPCName;
        CreateDialogue();
    }

    public void CreateDialogue()
    {
        dialogueText.text = dialogueLines[dialogueIndex];
        nameText.text = nameOfNPC;
        dialoguePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinueDialogue()
    {
        if (dialogueIndex < dialogueLines.Count - 1)
        {
            dialogueIndex++;
            dialogueText.text = dialogueLines[dialogueIndex];
        }
        else
        {
            dialoguePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    void Start()
    {

    }

    
    void Update()
    {

    }
}
