using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    public List<DroppedItemSaveData> droppedItems = new();
    public List<TilemapSaveData> tilemapSaveData = new();
    public SceneData()
    {
    }

    public void Clear()
    {
        droppedItems.Clear();
        tilemapSaveData.Clear();
    }
}
