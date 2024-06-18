using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileIndicator : MonoBehaviour
{
    public Tilemap interactiveMap;

    public PlayerData playerData;

    private void HighlightTile()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerData.curDirection, 1.0f, LayerMask.GetMask("Tilemap"));
        if (hit.collider != null)
        {
            var tilemap = hit.collider.GetComponent<Tilemap>();
            Vector3Int highlightPos = tilemap.WorldToCell(hit.point);

            if (playerData.prevHighlightedPos != highlightPos)
            {
                interactiveMap.SetTile(playerData.prevHighlightedPos, null);
                interactiveMap.SetTile(highlightPos, playerData.hoverTile);
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
