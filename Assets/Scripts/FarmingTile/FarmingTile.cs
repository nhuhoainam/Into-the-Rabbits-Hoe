using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;

public class FarmingTile : MonoBehaviour
{
    [SerializeField] TileBase tilledTile;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void Interact(Vector3 position)
    {
        // Check if the player has seeds in their inventory
        // If they do, plant the seed
        // If they don't, harvest the crop

        Tilemap tilemap = GetComponent<Tilemap>();
        tilemap.SetTile(tilemap.WorldToCell(position), tilledTile);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
