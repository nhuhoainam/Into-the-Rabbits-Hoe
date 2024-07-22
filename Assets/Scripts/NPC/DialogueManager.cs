using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Animancer;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public static UnityAction<ShopKeeper> OnOpenShop;

    private string[] dialogue;
    private string npcName;
    private Sprite npcAvatar;
    public int dialogueIndex = 0; // Index of the current dialogue
    public float wordSpeed = 1f; // Speed of the text display
    public GameObject dialoguePanel; // Contains the dialogue box, name box, and avatar box
    public GameObject dialogueBox;
    public GameObject nameBox;
    public GameObject avatarBox;
    public GameObject shopButton;
    public bool isDialogueActive = false;

    [SerializeField] private ShopKeeper currentShopKeeper;

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    void Update()
    {

    }

    // Start the conversation, is called by the NPC script
    public void StartDialogue(string[] dialogue, string npcName, Sprite npcAvatar, ShopKeeper shopKeeper)
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
        shopButton.SetActive(false);
        NextText(shopKeeper);
    }

    // Display the next text
    public void NextText(ShopKeeper shopKeeper)
    {
        if (dialogueIndex < dialogue.Length)
        {
            StopAllCoroutines(); // Stop any ongoing text display
            StartCoroutine(DisplayText(dialogue[dialogueIndex]));
            if (shopKeeper != null && dialogueIndex == dialogue.Length - 1)
            {
                currentShopKeeper = shopKeeper;
                shopButton.SetActive(true);
            }
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
    // Create a function for the shop button onClick event
    public void OpenShop()
    {
        Debug.Log("Opening shop");
        EndDialogue();
        OnOpenShop?.Invoke(currentShopKeeper);
    }
}
