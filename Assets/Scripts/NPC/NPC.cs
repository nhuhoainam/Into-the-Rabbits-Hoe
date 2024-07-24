using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class NPC : MonoBehaviour, IPlayerInteractable
{
    public bool isShop;
    public string npcName;
    public Sprite avatar;
    public List<Quest> listOfQuests;
    public List<string> listOfQuestNames;
    public List<string> defaultDialogue;
    private bool isTriggered = false;
    private PlayerController playerController;
    private Database database;
    private PlayerInventoryHolder playerInventoryHolder;
    private ShopKeeper shopKeeper;

    private void SaveNPCData()
    {
        if (!SaveGameManager.CurrentSaveData.NPCQuests.ContainsKey(npcName))
        {
            SaveGameManager.CurrentSaveData.NPCQuests.Add(npcName, new NPCData(new List<string>(listOfQuestNames), new List<string>(defaultDialogue)));
        }
        else
        {
            SaveGameManager.CurrentSaveData.NPCQuests[npcName] = new NPCData(new List<string>(listOfQuestNames), new List<string>(defaultDialogue));
        }
    }
    private void LoadNPCData(SaveData saveData)
    {
        if (saveData.NPCQuests.ContainsKey(npcName))
        {
            listOfQuestNames = saveData.NPCQuests[npcName].questNames;
            defaultDialogue = saveData.NPCQuests[npcName].defaultDialogue;
        }
    }
    void Awake()
    {
        // Find the player and get the PlayerController script
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerInventoryHolder = GameObject.FindWithTag("Player").GetComponent<PlayerInventoryHolder>();
        shopKeeper = GetComponent<ShopKeeper>();
        database = Resources.Load<Database>("Database");
        SaveGameManager.OnSaveGame += SaveNPCData;
        SaveGameManager.OnLoadGame += LoadNPCData;
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
    }

    int IPlayerInteractable.Priority => 2;

    void IPlayerInteractable.Interact(IPlayerInteractable.InteractionContext ctx)
    {
        Debug.Log("Interacting with NPC");
        var dialogueManager = GameObject.FindWithTag("DialogueManager").GetComponent<DialogueManager>();
        if (dialogueManager == null)
        {
            Debug.Log("No dialogue manager");
            return;
        }
        // If the NPCs have no quests, display the default dialogue
        if (listOfQuestNames.Count == 0)
        {
            Debug.Log("No quests");
            dialogueManager.StartDialogue(defaultDialogue.ToArray(), npcName, avatar, shopKeeper);
        }
        // looking to see if the first quest in the npc is in player active quests
        else
        {
            foreach (var quest in playerController.playerData.activeQuests)
            {
                if (quest == listOfQuestNames[0])
                {
                    Debug.Log("Active quest: " + quest);
                    Quest questData = listOfQuests.Find(q => q.questName == quest);
                    if (questData.CanComplete(playerController, playerInventoryHolder, database))
                    {
                        playerController.playerData.activeQuests.Remove(quest);
                        playerController.playerData.completedQuests.Add(quest);
                        playerController.Money += questData.amountToReward;
                        foreach (var item in questData.rewardItems)
                        {
                            var itemData = database.GetItem(item.itemName);
                            playerInventoryHolder.AddToInventory(itemData, item.amount);
                        }
                        dialogueManager.StartDialogue(questData.endDialog, npcName, avatar, shopKeeper);
                        defaultDialogue = new List<string>(questData.newDefaultDialog);
                        listOfQuestNames.RemoveAt(0);
                        return;
                    }
                    else
                    {
                        dialogueManager.StartDialogue(questData.inProgressDialog, npcName, avatar, shopKeeper);
                        return;
                    }
                }
            }
            foreach (var quest in playerController.playerData.inactiveQuests)
            {
                if (quest == listOfQuestNames[0])
                {
                    Debug.Log("Inactive quest: " + quest);
                    Quest questData = listOfQuests.Find(q => q.questName == quest);
                    if (questData.CanStart(playerController, playerInventoryHolder, database))
                    {
                        playerController.playerData.inactiveQuests.Remove(quest);
                        playerController.playerData.activeQuests.Add(quest);
                        playerController.Money += questData.amountToGive;
                        foreach (var item in questData.startItems)
                        {
                            var itemData = database.GetItem(item.itemName);
                            playerInventoryHolder.AddToInventory(itemData, item.amount);
                        }
                        dialogueManager.StartDialogue(questData.startDialog, npcName, avatar, shopKeeper);
                        return;
                    }
                }
            }
            Debug.Log("Default dialogue");
            dialogueManager.StartDialogue(defaultDialogue.ToArray(), npcName, avatar, shopKeeper);
        }
    }

    ItemData IPlayerInteractable.RequiredItem(IPlayerInteractable.InteractionContext ctx)
    {
        return null;
    }
}
