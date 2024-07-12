using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NPC : MonoBehaviour
{
    public string npcName;
    public Sprite avatar;
    public List<Quest> listOfQuests;
    public string[] defaultDialogue;
    private bool isTriggered = false;
    private PlayerController playerController;
    private Database database;
    private PlayerInventoryHolder playerInventoryHolder;

    void Awake()
    {
        // Find the player and get the PlayerController script
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerInventoryHolder = GameObject.FindWithTag("Player").GetComponent<PlayerInventoryHolder>();
        database = Resources.Load<Database>("Database");
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
                    // If the NPCs have no quests, display the default dialogue
                    if (listOfQuests.Count == 0)
                    {
                        dialogueManager.StartDialogue(defaultDialogue, npcName, avatar);
                    }
                    // looking to see if the first quest in the npc is in player active quests
                    else {
                        foreach (var quest in playerController.playerData.activeQuests) {
                            if (quest == listOfQuests[0].questName && listOfQuests[0].CanComplete(playerController, playerInventoryHolder, database)) {
                                playerController.playerData.activeQuests.Remove(quest);
                                playerController.playerData.completedQuests.Add(quest);
                                playerController.playerData.money += listOfQuests[0].amountToReward;
                                foreach (var item in listOfQuests[0].rewardItems) {
                                    var itemData = database.GetItem(item.itemName);
                                    playerInventoryHolder.AddToInventory(itemData, item.amount);
                                }
                                dialogueManager.StartDialogue(listOfQuests[0].endDialog, npcName, avatar);
                                listOfQuests.RemoveAt(0);
                                return;
                            }
                            else if (quest == listOfQuests[0].questName && !listOfQuests[0].CanComplete(playerController, playerInventoryHolder, database)){
                                dialogueManager.StartDialogue(listOfQuests[0].inProgressDialog, npcName, avatar);
                                return;
                            }
                        }
                        foreach (var quest in playerController.playerData.inactiveQuests) {
                            if (quest == listOfQuests[0].questName && listOfQuests[0].CanStart(playerController, playerInventoryHolder, database)) {
                                playerController.playerData.money += listOfQuests[0].amountToGive;
                                foreach (var item in listOfQuests[0].startItems) {
                                    var itemData = database.GetItem(item.itemName);
                                    playerInventoryHolder.AddToInventory(itemData, item.amount);
                                }
                                playerController.playerData.activeQuests.Add(quest);
                                playerController.playerData.inactiveQuests.Remove(quest);
                                dialogueManager.StartDialogue(listOfQuests[0].startDialog, npcName, avatar);
                                return;
                            }
                        }
                        dialogueManager.StartDialogue(defaultDialogue, npcName, avatar);
                    }
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
