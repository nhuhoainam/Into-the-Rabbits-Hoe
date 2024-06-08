using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public Tilemap obstacleTilemap;

    public List<Vector3Int> FindPath(Vector3Int start, Vector3Int end)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        // Implement your pathfinding logic here.
        // This is a placeholder for an A* or similar algorithm.

        // For demonstration purposes, we'll use a simple direct path:
        path.Add(start);
        path.Add(end);

        return path;
    }

    private bool IsWalkable(Vector3Int position)
    {
        return !obstacleTilemap.HasTile(position);
    }
}
