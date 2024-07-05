using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileIndicator : MonoBehaviour
{
    public Tilemap interactiveMap;
    public PlayerController playerData;
    [SerializeField] private Tile hoverTile;

    private void HighlightTile()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerData.Direction, 1.0f, LayerMask.GetMask("Tilemap"));
        if (hit.collider != null)
        {
            var tilemap = hit.collider.GetComponent<Tilemap>();
            Vector3Int highlightPos = tilemap.WorldToCell(hit.point);

            if (playerData.prevHighlightedPos != highlightPos)
            {
                interactiveMap.SetTile(playerData.prevHighlightedPos, null);
                interactiveMap.SetTile(highlightPos, hoverTile);
                playerData.prevHighlightedPos = highlightPos;
            }
        } else {
            interactiveMap.SetTile(playerData.prevHighlightedPos, null);
        }
    }

    private void Update()
    {
        HighlightTile();
    }
}
