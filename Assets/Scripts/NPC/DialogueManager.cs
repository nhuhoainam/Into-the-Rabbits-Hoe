using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private string[] dialogue;
    private string npcName;
    private Sprite npcAvatar;
    public int dialogueIndex = 0; // Index of the current dialogue
    public float wordSpeed = 1f; // Speed of the text display
    public GameObject dialoguePanel; // Contains the dialogue box, name box, and avatar box
    public GameObject dialogueBox;
    public GameObject nameBox;
    public GameObject avatarBox;
    public bool isDialogueActive = false;

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    void Update()
    {

    }

    // Start the conversation, is called by the NPC script
    public void StartDialogue(string[] dialogue, string npcName, Sprite npcAvatar)
    {
        this.dialogue = dialogue;
        this.npcName = npcName;
        this.npcAvatar = npcAvatar;
        nameBox.GetComponent<TextMeshProUGUI>().text = npcName;
        avatarBox.GetComponent<Image>().sprite = npcAvatar;
        var player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        player.DisableInput();
        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        NextText();
    }

    // Display the next text
    public void NextText()
    {
        if (dialogueIndex < dialogue.Length)
        {
            StopAllCoroutines(); // Stop any ongoing text display
            StartCoroutine(DisplayText(dialogue[dialogueIndex]));
            dialogueIndex++;
        }
        else {
            EndDialogue();
        }
    }

    // Display the text character by character
    private IEnumerator DisplayText(string text)
    {
        dialogueBox.GetComponent<TextMeshProUGUI>().text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueBox.GetComponent<TextMeshProUGUI>().text += letter;
            yield return new WaitForSeconds(wordSpeed / 100);
        }
    }

    // End the conversation
    public void EndDialogue()
    {
        isDialogueActive = false;
        dialogueBox.GetComponent<TextMeshProUGUI>().text = "";
        dialogueIndex = 0;
        dialoguePanel.SetActive(false);
        var player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        player.EnableInput();
    }
}
