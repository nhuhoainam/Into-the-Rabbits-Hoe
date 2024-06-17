using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class GrassTilemap : MonoBehaviour
{
    public Tilemap grassTilemap;

    void Start()
    {
        if (grassTilemap == null)
        {
            grassTilemap = GetComponent<Tilemap>();
        }
    }

    public IEnumerator DeleteSurroundingGrass(Vector3Int position, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Delete the grass tile at the collision position and its surroundings
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector3Int tilePosition = new Vector3Int(position.x + x, position.y + y, position.z);
                if (grassTilemap.HasTile(tilePosition))
                {
                    grassTilemap.SetTile(tilePosition, null);
                    Debug.Log($"Deleted tile at: {tilePosition}");
                }
            }
        }
    }
}
