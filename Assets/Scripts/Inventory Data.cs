using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventoryData : ScriptableObject
{
    public const int maxNoItems = 36;
    public List<ItemInstance> items = new();

    public const int maxActiveItems = 9;
    public int currentSlot = 0;
    public List<ItemInstance> activeItems = new();

    public InventoryData() {
        for (int i = 0; i < maxNoItems; i++) {
            items.Add(null);
        }
        for (int i = 0; i < maxActiveItems; i++) {
            activeItems.Add(null);
        }
    }

    public bool AddItem(ItemInstance newItem) {
        for (int i = 0; i < maxNoItems; i++) {
            if (items[i] == null) {
                items[i] = newItem;
                return true;
            }
        }
        return false;
    }

    public bool AddActiveItem(ItemInstance newItem) {
        for (int i = 0; i < maxActiveItems; i++) {
            if (activeItems[i] == null) {
                activeItems[i] = newItem;
                return true;
            }
        }
        return false;
    }

    public void RemoveItem(int index) {
        items[index] = null;
    }

    public void RemoveActiveItem(int index) {
        activeItems[index] = null;
    }
}
