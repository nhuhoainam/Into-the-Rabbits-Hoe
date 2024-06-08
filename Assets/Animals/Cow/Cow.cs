using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class Cow : MonoBehaviour
{
    public Tilemap grassTilemap;
    public float detectionRadius = 5f;
    public float moveSpeed = 2f;
    public float searchInterval = 5f; // Time interval between searches in seconds

    private Vector3 targetPosition;
    private Vector3Int currentTargetTile;
    private bool moving = false;
    private GrassTilemap grassTilemapScript;
    private Rigidbody2D rb;

    void Start()
    {
        currentTargetTile = Vector3Int.zero;
        grassTilemapScript = grassTilemap.GetComponent<GrassTilemap>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(SearchForGrassRoutine());
    }

    void Update()
    {
        if (moving)
        {
            MoveTowardsTarget();
        }
    }

    IEnumerator SearchForGrassRoutine()
    {
        while (true)
        {
            FindNearestGrassTile();
            yield return new WaitForSeconds(searchInterval);
        }
    }

    void FindNearestGrassTile()
    {
        Vector3 cowPosition = transform.position;
        float nearestDistance = Mathf.Infinity;
        Vector3Int nearestGrassTile = Vector3Int.zero;

        // Check tiles within the detection radius
        for (int x = -Mathf.CeilToInt(detectionRadius); x <= Mathf.CeilToInt(detectionRadius); x++)
        {
            for (int y = -Mathf.CeilToInt(detectionRadius); y <= Mathf.CeilToInt(detectionRadius); y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0) + grassTilemap.WorldToCell(cowPosition);
                if (grassTilemap.HasTile(tilePosition) && tilePosition != currentTargetTile)
                {
                    float distance = Vector3.Distance(cowPosition, grassTilemap.CellToWorld(tilePosition));
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestGrassTile = tilePosition;
                    }
                }
            }
        }

        if (nearestDistance < Mathf.Infinity)
        {
            targetPosition = grassTilemap.CellToWorld(nearestGrassTile);
            currentTargetTile = nearestGrassTile;
            moving = true;
            Debug.Log($"Moving to nearest grass tile at: {nearestGrassTile}");
        }
        else
        {
            moving = false;
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        newPosition.z = 0; // Lock Z position to 0
        rb.MovePosition(newPosition);
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            moving = false;
            Debug.Log("Reached target position, starting deletion of surrounding grass.");
            StartCoroutine(grassTilemapScript.DeleteSurroundingGrass(currentTargetTile, 5f));
            currentTargetTile = Vector3Int.zero; // Reset the target tile once the cow reaches it
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Optional: Debug to verify collisions
        Debug.Log($"Collision detected with: {collision.gameObject.tag}");
    }
}
