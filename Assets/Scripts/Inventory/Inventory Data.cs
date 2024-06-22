using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData
{
    public const int maxNoItems = 36;
    public List<ItemInstance> items = new();

    public const int maxActiveItems = 9;
    public int currentSlot = 0;
    public List<ItemInstance> activeItems = new();

    public InventoryData()
    {
        for (int i = 0; i < maxNoItems; i++)
        {
            items.Add(null);
        }
        for (int i = 0; i < maxActiveItems; i++)
        {
            activeItems.Add(null);
        }
    }

    public bool AddItem(ItemInstance newItem, out bool addedToActive)
    {
        addedToActive = AddActiveItem(newItem);
        if (addedToActive)
        {
            return true;
        }
        addedToActive = false;
        for (int i = 0; i < maxNoItems; i++)
        {
            if (items[i] == null)
            {
                items[i] = newItem;
                return true;
            }
        }
        return false;
    }

    public bool AddItem(ItemInstance newItem)
    {
        if (newItem.itemData.isStackable == true)
        {
            for (int i = 0; i < maxNoItems; i++)
            {
                if (items[i] == null)
                {
                    continue;
                }
                if (items[i].itemData.itemID == newItem.itemData.itemID)
                {
                    items[i].quantity += newItem.quantity;
                    return true;
                }
            }
        }
        for (int i = 0; i < maxNoItems; i++)
        {
            if (items[i] == null)
            {
                items[i] = newItem;
                return true;
            }
        }
        return false;
    }

    public bool AddActiveItem(ItemInstance newItem)
    {
        if (newItem.itemData.isStackable == true)
        {
            for (int i = 0; i < maxActiveItems; i++)
            {
                if (activeItems[i] == null)
                {
                    continue;
                }
                if (activeItems[i].itemData.itemID == newItem.itemData.itemID)
                {
                    activeItems[i].quantity += newItem.quantity;
                    return true;
                }
            }
        }
        for (int i = 0; i < maxActiveItems; i++)
        {
            if (activeItems[i] == null)
            {
                activeItems[i] = newItem;
                return true;
            }
        }
        return false;
    }

    public void RemoveItem(int index)
    {
        items[index] = null;
    }

    public void RemoveActiveItem(int index)
    {
        activeItems[index] = null;
    }
}
