using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class AssignUUIDTilemap : Editor
{
    [MenuItem("Assets/Assign UUID to Tilemap")]
    public static void AssignUUID()
    {
        var tilemaps = FindObjectsOfType<Tilemap>();
        foreach (var tilemap in tilemaps)
        {
            if (tilemap.GetComponent<UniqueID>() == null)
            {
                tilemap.gameObject.AddComponent<UniqueID>();
            }
            if (tilemap.TryGetComponent<FarmingTile>(out var farming))
            {
                farming.InitFarmTiles();
            }
        }
    }
}