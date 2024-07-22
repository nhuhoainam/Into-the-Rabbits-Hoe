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
    public TextMeshProUGUI nameBox;
    public Image avatarBox;
    public GameObject shopButton;
    public bool isDialogueActive = false;
    private PlayerControls playerControls;
    [SerializeField] private ShopKeeper currentShopKeeper;

    void Awake() {
        playerControls = new PlayerControls();
    }
    void Start()
    {
        dialoguePanel.SetActive(false);
        playerControls.Interaction.NextText.performed += ctx => NextText(currentShopKeeper);
    }

    void OnEnable() {
        playerControls.Interaction.Enable();
    }

    void OnDisable() {
        playerControls.Interaction.Disable();
    }

    void Update()
    {
    }

    // Start the conversation, is called by the NPC script
    public void StartDialogue(string[] dialogue, string npcName, Sprite npcAvatar, ShopKeeper shopKeeper)
    {
        if (isDialogueActive)
        {
            return;
        }
        this.dialogue = dialogue;
        this.npcName = npcName;
        this.npcAvatar = npcAvatar;
        nameBox.text = npcName;
        avatarBox.sprite = npcAvatar;
        var player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        player.DisableInput();
        isDialogueActive = true;
        dialogueIndex = 0;
        shopButton.SetActive(false);
        NextText(shopKeeper);
    }

    // Display the next text
    private void NextText(ShopKeeper shopKeeper)
    {
        if (isDialogueActive == false)
        {
            return;
        }
        if (dialoguePanel.activeSelf == false)
        {
            dialoguePanel.SetActive(true);
        }
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
        Debug.Log("Ending dialogue");
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
