using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapSaveHandler : Singleton<TilemapSaveHandler> {
    Dictionary<string, Tilemap> tilemaps = new();

    [SerializeField] BoundsInt bounds;

    private void Start() {
        InitTilemaps();
        SaveGameManager.OnSaveGame += OnSave;
        SaveGameManager.OnLoadGame += OnLoad;
    }

    private void InitTilemaps() {
        // get all tilemaps from scene
        // and write to dictionary
        Tilemap[] maps = FindObjectsOfType<Tilemap>();

        // the hierarchy name must be unique
        // you might add some checks here to make sure
        foreach (var map in maps) {
            // if you have tilemaps you don't want to safe - filter them here
            tilemaps.Add(map.name, map);
        }
    }

    public void OnSave() {
        // List that will later be safed
        List<TilemapSaveData> data = new();

        // foreach existing tilemap
        foreach (var mapObj in tilemaps) {
            TilemapSaveData mapData = new()
            {
                key = mapObj.Key
            };

            data.Add(mapData);
        }
        SaveGameManager.CurrentSaveData.tilemapSaveData = data;
    }

    public void OnLoad(SaveData data) {
        foreach (var mapData in data.tilemapSaveData) {
            // if key does NOT exist in dictionary skip it
            if (!tilemaps.ContainsKey(mapData.key)) {
                Debug.LogError("Found saved data for tilemap called '" + mapData.key + "', but Tilemap does not exist in scene.");
                continue;
            }

            // get according map
            var map = tilemaps[mapData.key];

            // clear map
            map.ClearAllTiles();

            if (mapData.tiles != null && mapData.tiles.Count > 0) {
                foreach (var tile in mapData.tiles) {
                    map.SetTile(tile.position, tile.tile);
                }
            }
        }
    }
}