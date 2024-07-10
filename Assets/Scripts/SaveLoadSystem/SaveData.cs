using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public InventorySaveData playerInventory;
    public PlayerData playerData;
    public List<DroppedItemSaveData> droppedItems = new();

    public SaveData()
    {
    }
}
