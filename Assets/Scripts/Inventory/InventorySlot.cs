using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventorySlot : ISerializationCallbackReceiver
{
    [NonSerialized] private ItemData _itemData; // Don't use  this directly, use ItemData property
    [SerializeField] private int itemID = -1;
    [SerializeField] private int stackSize;

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

    public InventorySlot(ItemData source, int amount)
    {
        ItemData = source;
        stackSize = amount;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

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

    public void UpdateInventorySlot(ItemData source, int amount)
    {
        ItemData = source;
        stackSize = amount;
    }

    public bool EnoughRoomInStack(int amountToAdd)
    {
        return ItemData == null || (ItemData != null && stackSize + amountToAdd <= ItemData.maxStackSize);
    }

    public bool EnoughRoomInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = (int)(ItemData.maxStackSize - stackSize);
        return EnoughRoomInStack(amountToAdd);
    }

    public bool RoomLeftInStack(uint amountToAdd, out uint amountRemaining)
    {

        amountRemaining = (uint)(ItemData.maxStackSize - stackSize);

        RoomLeftInStack(amountToAdd);

        return RoomLeftInStack(amountToAdd);
    }

    public bool RoomLeftInStack(uint amountToAdd)
    {

        if (stackSize + amountToAdd <= ItemData.maxStackSize) return true;
        else return false;
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }

    public bool SplitStack(out InventorySlot splitStack)
    {
        if (stackSize <= 1)
        {

            splitStack = null;
            return false;
        }


        int halfStack = Mathf.RoundToInt(stackSize / 2);
        RemoveFromStack(halfStack);

        splitStack = new InventorySlot(ItemData, halfStack);
        return true;
    }

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        if (itemID != -1) return;

        var db = Resources.Load<ItemDatabase>("Database");
        ItemData = db.GetItem(itemID);
    }
}

