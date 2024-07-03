using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public ItemData item;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerInventoryHolder inventoryHolder))
        {
            if (inventoryHolder.PrimaryInventorySystem.AddToInventory(item, 1)) {
                Debug.Log("Item added to inventory");
                Destroy(gameObject);
            }
        }
    }
}
