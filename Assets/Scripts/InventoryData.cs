using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventoryData : ScriptableObject
{
    public const int maxNoItems = 36;
    public List<ItemInstance> items = new();

    public InventoryData() {
        for (int i = 0; i < maxNoItems; i++) {
            items.Add(null);
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
}
