using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSlot : ISerializationCallbackReceiver
{
    [NonSerialized] protected ItemData _itemData; // Don't use  this directly, use ItemData property
    [SerializeField] protected int itemID = -1;
    [SerializeField] protected int stackSize;

    public ItemData ItemData
    {
        get => _itemData;
        set
        {
            _itemData = value;
            if (value != null)
                itemID = value.itemID;
            else
                itemID = -1;
        }
    }
    public int StackSize => stackSize;

    public void ClearSlot()
    {
        ItemData = null;
        stackSize = -1;
    }

    public void AssignItem(InventorySlot invSlot)
    {
        if (ItemData == invSlot.ItemData) AddToStack(invSlot.stackSize);
        else
        {
            ItemData = invSlot.ItemData;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }

    public void AssignItem(ItemData data, int amount)
    {
        if (ItemData == data) AddToStack(amount);
        else
        {
            ItemData = data;
            stackSize = 0;
            AddToStack(amount);
        }
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        if (itemID == -1) return;

        var db = Resources.Load<Database>("Database");
        ItemData = db.GetItem(itemID);
    }
}