using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopSlot
{
    public ItemData item;

    public ShopSlot(ItemData item)
    {
        this.item = item;
    }
}