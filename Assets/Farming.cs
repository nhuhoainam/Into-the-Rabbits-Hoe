using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Farming : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject obj = GameObject.FindWithTag("FarmableGround");
        FarmingTile tilemap = obj.GetComponent<FarmingTile>();
        if (Input.GetKeyUp(KeyCode.F))
        {
            tilemap.Interact(player.position);
        }
    }
}
