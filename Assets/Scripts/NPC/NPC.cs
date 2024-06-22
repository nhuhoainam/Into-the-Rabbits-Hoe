using System.Collections;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public string npcName;
    public Sprite avatar;
    public string[] dialogue;
    private bool isTriggered = false;
    private PlayerController playerController;

    void Awake()
    {
        // Find the player and get the PlayerController script
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = false;
        }
    }

    void Update()
    {
        if (isTriggered && Input.GetKeyDown(KeyCode.Space))
        {
            var dialogueManager = GameObject.FindWithTag("DialogueManager").GetComponent<DialogueManager>();
            if (dialogueManager != null)
            {
                if (!dialogueManager.isDialogueActive)
                {
                    dialogueManager.StartDialogue(dialogue, npcName, avatar);
                    dialogueManager.NextText();
                }
                else {
                    dialogueManager.NextText();
                }
            }
            else
            {
                Debug.LogWarning("No DialogueManager found in the scene");
            }
        }
    }
}
