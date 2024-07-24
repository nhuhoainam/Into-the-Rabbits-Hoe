using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapSaveHandler : Singleton<TilemapSaveHandler>
{
    Dictionary<string, Tilemap> tilemaps = new();

    private void Start()
    {
        SaveGameManager.OnSaveScene += OnSave;
        SaveGameManager.OnLoadScene += OnLoad;
    }

    private void InitTilemaps()
    {
        tilemaps = new();
        // get all tilemaps from scene
        // and write to dictionary
        Tilemap[] maps = FindObjectsOfType<Tilemap>();

        // the hierarchy name must be unique
        // you might add some checks here to make sure
        foreach (var map in maps)
        {
            // if you have tilemaps you don't want to safe - filter them here
            tilemaps.Add(map.GetComponent<UniqueID>().ID, map);
        }
    }

    public void OnSave(int sceneIndex)
    {
        InitTilemaps();
        // List that will later be safed
        List<TilemapSaveData> data = new();

        // foreach existing tilemap
        foreach (var mapObj in tilemaps)
        {
            TilemapSaveData mapData = new()
            {
                key = mapObj.Key,
                tiles = new List<TileSaveData>()
            };
            for (int x = mapObj.Value.cellBounds.xMin; x < mapObj.Value.cellBounds.xMax; x++)
            {
                for (int y = mapObj.Value.cellBounds.yMin; y < mapObj.Value.cellBounds.yMax; y++)
                {
                    for (int z = mapObj.Value.cellBounds.zMin; z < mapObj.Value.cellBounds.zMax; z++)
                    {
                        Vector3Int pos = new(x, y, z);
                        TileBase tile = mapObj.Value.GetTile(pos);
                        if (tile != null)
                        {
                            mapData.tiles.Add(new TileSaveData(tile, pos));
                        }
                    }
                }
            }

            data.Add(mapData);
        }
        SaveGameManager.CurrentSaveData.sceneData[sceneIndex].tilemapSaveData = data;
    }

    public void OnLoad(SaveData data, int sceneIndex)
    {
        InitTilemaps();
        foreach (var mapData in data.sceneData[sceneIndex].tilemapSaveData)
        {
            // if key does NOT exist in dictionary skip it
            if (!tilemaps.ContainsKey(mapData.key))
            {
                Debug.LogError("Found saved data for tilemap called '" + mapData.key + "', but Tilemap does not exist in scene.");
                continue;
            }

            // get according map
            var map = tilemaps[mapData.key];

            // clear map
            map.ClearAllTiles();

            if (mapData.tiles != null && mapData.tiles.Count > 0)
            {
                foreach (var tile in mapData.tiles)
                {
                    map.SetTile(tile.position, tile.tile);
                }
            }
        }
    }
}