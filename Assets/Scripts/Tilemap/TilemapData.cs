using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class TilemapSaveData
{
    public string key;
    public List<TileSaveData> tiles = new();
}

[System.Serializable]
public class TileSaveData
{
    public TileBase tile;
    public Vector3Int position;

    public TileSaveData(TileBase tile, Vector3Int position)
    {
        this.tile = tile;
        this.position = position;
    }
}