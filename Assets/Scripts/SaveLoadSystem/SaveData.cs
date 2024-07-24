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
    public int currentScene = 0;
    public PlayerData playerData;
    public SerializableDictionary<int, SceneData> sceneData = new();
    public SerializableDictionary<string, NPCData> NPCQuests = new();
    public SaveData()
    {
    }
}
