using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour
{
    public string npcName;
    public Sprite avatar;
    public Quest[] listOfQuests;
    private Quest currentQuest;
    public string[] defaultDialogue;
    private bool isTriggered = false;
    private PlayerController playerController;
    private Database itemDatabase;
    private PlayerInventoryHolder playerInventoryHolder;

    void Awake()
    {
        // Find the player and get the PlayerController script
        var player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerInventoryHolder = player.GetComponent<PlayerInventoryHolder>();
        itemDatabase = Resources.Load<Database>("Database");
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
        if (isTriggered && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            var dialogueManager = GameObject.FindWithTag("DialogueManager").GetComponent<DialogueManager>();
            if (dialogueManager != null)
            {
                if (!dialogueManager.isDialogueActive)
                {
                    if (currentQuest == null || currentQuest.status == QuestStatus.Completed) {
                        currentQuest = GetNextQuest();
                        if (currentQuest != null)
                        {
                            currentQuest.status = QuestStatus.Active;
                            playerController.playerData.money += currentQuest.startMoney;
                            for (int i = 0; i < currentQuest.startItems.Count; i++)
                            {
                                var itemData = itemDatabase.GetItem(currentQuest.startItems[i].name);
                                playerInventoryHolder.AddToInventory(itemData, currentQuest.startItems[i].amount);
                            }
                            dialogueManager.StartDialogue(currentQuest.startDialogue, npcName, avatar);
                        }
                        else
                        {
                            dialogueManager.StartDialogue(defaultDialogue, npcName, avatar);
                        }
                    }
                    else if (currentQuest != null && currentQuest.status == QuestStatus.Active)
                    {
                        if (CheckCompletion())
                        {
                            currentQuest.status = QuestStatus.Completed;
                            playerController.playerData.money += currentQuest.rewardMoney;
                            for (int i = 0; i < currentQuest.rewardItems.Count; i++)
                            {
                                var itemData = itemDatabase.GetItem(currentQuest.rewardItems[i].name);
                                playerInventoryHolder.AddToInventory(itemData, currentQuest.rewardItems[i].amount);
                            }
                            dialogueManager.StartDialogue(currentQuest.endDialogue, npcName, avatar);
                        }
                        else
                        {
                            dialogueManager.StartDialogue(currentQuest.progressDialogue, npcName, avatar);
                        }
                    }
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
    private Quest GetNextQuest()
    {
        foreach (var quest in listOfQuests)
        {
            if (quest.status == QuestStatus.Inactive)
            {
                return quest;
            }
        }
        return null;
    }

    private bool CheckCompletion()
    {
        if (currentQuest.questType == QuestType.Planting)
        {
            return playerController.playerData.treesPlanted >= currentQuest.required;
        }
        else if (currentQuest.questType == QuestType.EarnMoney)
        {
            return playerController.playerData.money >= currentQuest.required;
        }
        return false;
    }
}
