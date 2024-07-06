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
        TileBase tile = tilemap.GetTile(tilemap.WorldToCell(position));
        if (tile == null)
        {
            tilemap.SetTile(tilemap.WorldToCell(position), tilledTile);
        }
        else if (tile == tilledTile)
        {
            Vector3 pos = tilemap.WorldToCell(position);
            pos += new Vector3(0.5f, 0.5f, 6);
            CropFactory.Instance.CreateCrop(CropFactory.CropType.Carrot, pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
