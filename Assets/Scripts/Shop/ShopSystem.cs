using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ShopSystem
{
    public List<ShopSlot> shopInventory;
    public float buyMarkup;
    public float sellMarkup;

    public int ShopSize
    {
        get => shopInventory.Count;
    }

    public ShopSystem(float buyMarkup, float sellMarkup)
    {
        shopInventory = new List<ShopSlot>();
        this.buyMarkup = buyMarkup;
        this.sellMarkup = sellMarkup;
    }

    public void AddToShop(ShopSlot itemToAdd)
    {
        shopInventory.Add(itemToAdd);
    }
}
