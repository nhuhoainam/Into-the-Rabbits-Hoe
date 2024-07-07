using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;

public class FarmingTile : MonoBehaviour
{
    [SerializeField] TileBase tilledTile;
    [SerializeField] TileBase grassTile;
    Tilemap grassTilemap;
    Tilemap tilemap;
    Tilemap tilledTilemap;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        grassTilemap = GameObject.FindWithTag("Grass").GetComponent<Tilemap>();
        tilledTilemap = GameObject.FindWithTag("TilledDirt").GetComponent<Tilemap>();
        tilledTilemap.GetComponent<TilemapRenderer>().sortingLayerName = tilemap.GetComponent<TilemapRenderer>().sortingLayerName;
        tilledTilemap.GetComponent<TilemapRenderer>().sortingOrder = tilemap.GetComponent<TilemapRenderer>().sortingOrder + 1;
    }

    void Till(Vector3 position)
    {
        // Check if the player has a hoe in their inventory
        // Check if the tile is grass -> remove it -> done
        // Check if the tile is tilled -> do nothing
        // Check if the tile is a crop -> do nothing
        // Otherwise, till the tile
        Debug.Log("Tilling");

        Debug.Log("Grass at position: " + GrassAt(position));
        if (GrassAt(position))
        {
            grassTilemap.SetTile(tilemap.WorldToCell(position), null);
            return;
        }
        Debug.Log("Tilled at position: " + TilledAt(position));

        if (TilledAt(position))
        {
            return;
        }

        tilledTilemap.SetTile(tilemap.WorldToCell(position), tilledTile);
    }

    bool TilledAt(Vector3 position)
    {
        TileBase tile = tilledTilemap.GetTile(tilledTilemap.WorldToCell(position));
        if (tile == null)
        {
            return false;
        }
        else if (tile == tilledTile)
        {
            return true;
        }
        return false;
    }

    bool GrassAt(Vector3 position)
    {
        TileBase tile = grassTilemap.GetTile(grassTilemap.WorldToCell(position));
        if (tile == null)
        {
            return false;
        }
        else if (tile == grassTile)
        {
            return true;
        }
        return true;
    }

    public void Interact(Vector3 position)
    {
        // Check if the player has seeds in their inventory
        // If they do, plant the seed
        // If they don't, harvest the crop

        if (TilledAt(position))
        {
            Vector3 pos = tilemap.WorldToCell(position);
            pos += new Vector3(0.5f, 0.5f, 6);
            CropFactory.Instance.CreateCrop(CropFactory.CropType.Carrot, pos, Quaternion.identity);
        }
        else
        {
            Till(position);
        }
        return;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
