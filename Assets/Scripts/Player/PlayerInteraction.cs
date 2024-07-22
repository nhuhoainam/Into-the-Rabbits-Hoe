using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    Rigidbody2D rigi;
    InventoryHolder inventoryHolder;
    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        inventoryHolder = GetComponent<InventoryHolder>();
    }
}
