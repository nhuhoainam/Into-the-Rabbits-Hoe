using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ShopSystem
{
    public List<ItemData> shopInventory;
    public float buyMarkup;
    public float sellMarkup;

    public int ShopSize
    {
        get => shopInventory.Count;
    }

    public ShopSystem(float buyMarkup, float sellMarkup)
    {
        this.buyMarkup = buyMarkup;
        this.sellMarkup = sellMarkup;
    }

    public bool ContainsItem(ItemData itemToCheck, out ItemData slots)
    {
        slots = shopInventory.Find(slot => slot == itemToCheck);
        return slots != null;
    }

    public void AddToShop(ItemData itemToAdd)
    {
        shopInventory.Add(itemToAdd);
    }
}
