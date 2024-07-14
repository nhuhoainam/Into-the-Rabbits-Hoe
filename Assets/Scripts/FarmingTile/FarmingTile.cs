using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    public TileBase grassTile;
    [SerializeField] TileBase fertilizerTile;
    Tilemap grassTilemap;
    Tilemap tilemap;
    Tilemap tilledTilemap;
    Tilemap wateredTilemap;
    Tilemap fertilizerTilemap;

    List<CropTile> crops = new();
    // Start is called before the first frame update

    void LoadTileResources()
    {
        if (tilledTile == null)
        {
            tilledTile = Resources.Load<TileBase>("Tilled_Dirt_Wide_RuleTile");
        }
        if (fertilizerTile == null)
        {
            fertilizerTile = Resources.Load<TileBase>("Fertilizer_RuleTile");
        }
    }

    void Start()
    {
        LoadTileResources();
        tilemap = GetComponent<Tilemap>();
        try
        {
            tilledTilemap = transform.GetChild(0).GetComponent<Tilemap>();
        }
        catch (Exception)
        {
            var newObj = Instantiate<GameObject>(new GameObject(), transform);
            newObj.AddComponent<Tilemap>();
            newObj.AddComponent<TilemapRenderer>();
            newObj.transform.SetParent(transform);
            tilledTilemap = newObj.GetComponent<Tilemap>();
        }

        try
        {
            wateredTilemap = transform.GetChild(1).GetComponent<Tilemap>();
        }
        catch (Exception)
        {
            var newObj = Instantiate(new GameObject(), transform);
            newObj.AddComponent<Tilemap>();
            newObj.AddComponent<TilemapRenderer>();
            newObj.transform.SetParent(transform);
            wateredTilemap = newObj.GetComponent<Tilemap>();
        }

        try
        {
            fertilizerTilemap = transform.GetChild(2).GetComponent<Tilemap>();
        }
        catch (System.Exception)
        {
            var newObj = Instantiate(new GameObject(), transform);
            newObj.AddComponent<Tilemap>();
            newObj.AddComponent<TilemapRenderer>();
            newObj.transform.SetParent(transform);
            fertilizerTilemap = newObj.GetComponent<Tilemap>();
        }

        grassTilemap = GameObject.FindWithTag("Grass").GetComponent<Tilemap>();

        tilledTilemap.GetComponent<TilemapRenderer>().sortingLayerName = tilemap.GetComponent<TilemapRenderer>().sortingLayerName;
        tilledTilemap.GetComponent<TilemapRenderer>().sortingOrder = tilemap.GetComponent<TilemapRenderer>().sortingOrder + 1;

        wateredTilemap.GetComponent<TilemapRenderer>().sortingLayerName = tilemap.GetComponent<TilemapRenderer>().sortingLayerName;
        wateredTilemap.GetComponent<TilemapRenderer>().sortingOrder = tilemap.GetComponent<TilemapRenderer>().sortingOrder + 2;
        wateredTilemap.GetComponent<Tilemap>().color = new(0.7924528f, 0.4539565f, 0.2280171f, 0.5f);

        fertilizerTilemap.GetComponent<TilemapRenderer>().sortingLayerName = tilemap.GetComponent<TilemapRenderer>().sortingLayerName;
        fertilizerTilemap.GetComponent<TilemapRenderer>().sortingOrder = tilemap.GetComponent<TilemapRenderer>().sortingOrder + 3;

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

    bool WateredAt(Vector3 position)
    {
        TileBase tile = wateredTilemap.GetTile(wateredTilemap.WorldToCell(position));
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
            if (WateredAt(position))
            {
                if (FertilizedAt(position))
                {
                    // Debug.Log("Harvesting");
                    PlantCrop(position, CropFactory.cropTypeDictionary[10]);
                    // crop.Interact();
                    return;
                }
                Fertilize(position);
                return;
            }
            Water(position);
            if (crop == null)
            {
                //     Debug.Log("Planting");
                //     Vector3Int posInt = tilemap.WorldToCell(position);
                //     Vector3 pos = (Vector3)posInt;
                //     pos += new Vector3(0.5f, 0.4f, 0);
                //     var sortingLayer = tilemap.GetComponent<TilemapRenderer>().sortingLayerID;
                //     var sortingOrder = tilemap.GetComponent<TilemapRenderer>().sortingOrder;
                //     var newCrop = CropFactory.GetInstance().CreateCrop(CropFactory.CropType.Carrot, pos, Quaternion.identity, sortingLayer, sortingOrder + 2);
                //     crops.Add(new(posInt, newCrop.GetComponent<Crop>()));
                //     Debug.Log("Crop planted at: " + posInt);
            }
            else
            {
                Debug.Log("Harvesting");
                // crop.Interact();
            }
        }
        else
        {
            Till(position);
        }
        return;
    }

    void PlantCrop(Vector3 position, CropFactory.CropType cropType)
    {
        Debug.Log("Planting");
        Vector3Int posInt = tilemap.WorldToCell(position);
        Vector3 pos = (Vector3)posInt;
        pos += new Vector3(0.5f, 0.4f, 0);
        var sortingLayer = tilemap.GetComponent<TilemapRenderer>().sortingLayerID;
        var sortingOrder = tilemap.GetComponent<TilemapRenderer>().sortingOrder;
        var newCrop = CropFactory.GetInstance().CreateCrop(cropType, pos, Quaternion.identity, sortingLayer, sortingOrder + 4);
        crops.Add(new(posInt, newCrop.GetComponent<Crop>()));
        Debug.Log("Crop planted at: " + posInt);
    }

    void Fertilize(Vector3 position)
    {
        Debug.Log("Fertilizing");
        if (TilledAt(position))
        {
            fertilizerTilemap.SetTile(tilemap.WorldToCell(position), fertilizerTile);
        }
        else
        {
            Crop crop = GetCropAt(position);
            if (crop != null)
            {
                // crop.Fertilize();
            }
        }
    }

    bool FertilizedAt(Vector3 position)
    {
        TileBase tile = fertilizerTilemap.GetTile(fertilizerTilemap.WorldToCell(position));
        if (tile == null)
        {
            return false;
        }
        else if (tile == fertilizerTile)
        {
            return true;
        }
        return false;
    }

    void Water(Vector3 position)
    {
        // Check if the player has a watering can in their inventory
        // Check if the tile is tilled -> water it
        // Check if the tile is a crop -> water it
        // Otherwise, do nothing
        Debug.Log("Watering");
        if (TilledAt(position))
        {
            wateredTilemap.SetTile(tilemap.WorldToCell(position), tilledTile);
        }
        else
        {
            Crop crop = GetCropAt(position);
            if (crop != null)
            {
                // crop.Water();
            }
        }
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
