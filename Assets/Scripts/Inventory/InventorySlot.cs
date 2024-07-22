using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventorySlot : ItemSlot
{
    public InventorySlot(ItemData source, int amount)
    {
        ItemData = source;
        stackSize = amount;
    }

    public InventorySlot()
    {
        ClearSlot();
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
}

