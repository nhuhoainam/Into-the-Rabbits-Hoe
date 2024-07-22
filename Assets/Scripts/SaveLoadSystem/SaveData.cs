using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCData {
    public List<string> questNames;
    public List<string> defaultDialogue;

    public NPCData(List<string> questNames, List<string> defaultDialogue) {
        this.questNames = questNames;
        this.defaultDialogue = defaultDialogue;
    }
}

[System.Serializable]
public class SaveData
{
    public InventorySaveData playerInventory;
    public PlayerData playerData;
    public List<DroppedItemSaveData> droppedItems = new();
    public List<TilemapSaveData> tilemapSaveData;
    public List<string> NPCQuestNames = new();
    public List<string> NPCDefaultDialogue = new();
    public SerializableDictionary<string, NPCData> NPCQuests = new();
    public SaveData()
    {
    }
}
