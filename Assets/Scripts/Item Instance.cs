using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstance
{
    public ItemData itemData;
    public uint quantity;

    public ItemInstance(ItemData itemData, uint quantity = 1)
    {
        this.itemData = itemData;
        this.quantity = quantity;
    }
}
