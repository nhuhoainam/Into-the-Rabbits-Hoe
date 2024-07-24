using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class QuestItem {
    public string itemName;
    public int amount;
}
[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    public string questName;
    public string questDescription;
    public QuestType questType;
    public int toStartAmount;
    public List<QuestItem> itemsToStart;
    public int amountRequired;
    public List<QuestItem> requiredItems;
    public int amountToGive;
    public List<QuestItem> startItems;
    public int amountToReward;
    public List<QuestItem> rewardItems;
    public string[] startDialog;
    public string[] inProgressDialog;
    public string[] endDialog;
    public string[] newDefaultDialog;
    public bool CanStart(PlayerController player, PlayerInventoryHolder inventoryHolder, Database database) {
        bool hasItems = true;
        foreach (var item in itemsToStart) {
            var itemData = database.GetItem(item.itemName);
            if (!inventoryHolder.ContainsItems(itemData, item.amount)) {
                return false;
            }
        }
        var enoughAmount = questType switch
        {
            QuestType.Planting => player.playerData.treesPlanted >= toStartAmount,
            QuestType.Earning => player.playerData.money >= toStartAmount,
            _ => false,
        };
        return hasItems && enoughAmount;
    }
    public bool CanComplete(PlayerController player, PlayerInventoryHolder inventoryHolder, Database database) {
        bool hasItems = true;
        foreach (var item in requiredItems) {
            var itemData = database.GetItem(item.itemName);
            if (!inventoryHolder.ContainsItems(itemData, item.amount)) {
                return false;
            }
        }
        var enoughAmount = questType switch
        {
            QuestType.Planting => player.playerData.treesPlanted >= amountRequired,
            QuestType.Earning => player.playerData.money >= amountRequired,
            _ => false,
        };
        return hasItems && enoughAmount;
    }
}

public enum QuestType {
    Planting,
    Earning
}
// public enum QuestStatus {
//     NotStarted,
//     InProgress,
//     Completed
// }
