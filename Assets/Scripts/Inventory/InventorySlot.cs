using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private int stackSize;

    public ItemData ItemData => itemData;
    public int StackSize => stackSize;

    public InventorySlot(ItemData source, int amount)
    {
        itemData = source;
        stackSize = amount;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        itemData = null;
        stackSize = -1;
    }

    public void AssignItem(InventorySlot invSlot)
    {
        if (itemData == invSlot.ItemData) AddToStack(invSlot.stackSize);
        else
        {
            itemData = invSlot.itemData;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }

    public void UpdateInventorySlot(ItemData source, int amount)
    {
        itemData = source;
        stackSize = amount;
    }

    public bool EnoughRoomInStack(int amountToAdd)
    {
        return itemData == null || (itemData != null && stackSize + amountToAdd <= itemData.maxStackSize);
    }

    public bool EnoughRoomInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = (int)(itemData.maxStackSize - stackSize);
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

        if (stackSize + amountToAdd <= itemData.maxStackSize) return true;
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

        splitStack = new InventorySlot(itemData, halfStack);
        return true;
    }
}

