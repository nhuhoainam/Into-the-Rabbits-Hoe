using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public ItemData item;
    public int amount;

    void Awake()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerInventoryHolder inventoryHolder))
        {
            if (inventoryHolder.PrimaryInventorySystem.AddToInventory(item, amount)) {
                Debug.Log("Item added to inventory");
                Destroy(gameObject);
            }
        }
    }
}
