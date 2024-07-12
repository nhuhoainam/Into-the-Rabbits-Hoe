using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;

public class FarmingTile : MonoBehaviour, IPlayerInteractable
{
    private class CropTile
    {
        public Vector3Int Position { get; }
        public Crop Crop { get; }
        public CropTile(Vector3Int position, Crop crop)
        {
            Position = position;
            Crop = crop;
        }
    }
    [SerializeField] TileBase tilledTile;
    [SerializeField] TileBase grassTile;
    [SerializeField] TileBase tilledDecorationTile;
    Tilemap grassTilemap;
    Tilemap tilemap;
    Tilemap tilledTilemap;
    Tilemap tilledDecorationTilemap;

    List<CropTile> crops = new();
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        try
        {
            tilledTilemap = transform.GetChild(0).GetComponent<Tilemap>();
        }
        catch (System.Exception)
        {
            var newObj = GameObject.Instantiate<GameObject>(new GameObject(), transform);
            newObj.AddComponent<Tilemap>();
            newObj.AddComponent<TilemapRenderer>();
            newObj.transform.SetParent(transform);
            tilledTilemap = newObj.GetComponent<Tilemap>();
        }

        try
        {
            tilledDecorationTilemap = transform.GetChild(1).GetComponent<Tilemap>();
        }
        catch (System.Exception)
        {
            var newObj = GameObject.Instantiate<GameObject>(new GameObject(), transform);
            newObj.AddComponent<Tilemap>();
            newObj.AddComponent<TilemapRenderer>();
            newObj.transform.SetParent(transform);
            tilledTilemap = newObj.GetComponent<Tilemap>();
        }

        grassTilemap = GameObject.FindWithTag("Grass").GetComponent<Tilemap>();
        tilledTilemap.GetComponent<TilemapRenderer>().sortingLayerName = tilemap.GetComponent<TilemapRenderer>().sortingLayerName;
        tilledTilemap.GetComponent<TilemapRenderer>().sortingOrder = tilemap.GetComponent<TilemapRenderer>().sortingOrder + 1;

        tilledDecorationTilemap.GetComponent<TilemapRenderer>().sortingLayerName = tilemap.GetComponent<TilemapRenderer>().sortingLayerName;
        tilledDecorationTilemap.GetComponent<TilemapRenderer>().sortingOrder = tilemap.GetComponent<TilemapRenderer>().sortingOrder + 2;
        this.gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    void IPlayerInteractable.Interact(PlayerData playerData)
    {
        // TODO: check if the player has a hoe in their inventory
        Interact(playerData.position);
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
        TileBase decorationTile = tilledDecorationTilemap.GetTile(tilledDecorationTilemap.WorldToCell(position));
        TileData data = new();
        decorationTile.GetTileData(new Vector3Int(0, 0, 0), tilledDecorationTilemap, ref data);
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
        foreach (var cropTile in crops)
        {
            Debug.Log("Crop at: " + cropTile.Position);
        }
        if (TilledAt(position))
        {
            Crop crop = GetCropAt(position);
            if (crop == null)
            {
                Debug.Log("Planting");
                Vector3Int posInt = tilemap.WorldToCell(position);
                Vector3 pos = (Vector3)posInt;
                pos += new Vector3(0.5f, 0.4f, 0);
                var sortingLayer = tilemap.GetComponent<TilemapRenderer>().sortingLayerID;
                var sortingOrder = tilemap.GetComponent<TilemapRenderer>().sortingOrder;
                var newCrop = CropFactory.GetInstance().CreateCrop(CropFactory.CropType.Carrot, pos, Quaternion.identity, sortingLayer, sortingOrder + 2);
                crops.Add(new(posInt, newCrop.GetComponent<Crop>()));
                Debug.Log("Crop planted at: " + posInt);
            }
            else
            {
                Debug.Log("Harvesting");
                crop.Interact();
            }
        }
        else
        {
            Till(position);
        }
        return;
    }

    Crop GetCropAt(Vector3 position)
    {
        Vector3Int posInt = tilemap.WorldToCell(position);
        CropTile crop = crops.Find((cropTile) => cropTile.Position == posInt);
        if (crop != null)
        {
            return crop.Crop;
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
