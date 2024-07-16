using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopSystem
{
    private List<ShopSlot> shopInventory;
    private int availableGold;
    private float buyMarkup;
    private float sellMarkup;

    public int ShopSize
    {
        get => shopInventory.Count;
        private set
        {
            shopInventory = new List<ShopSlot>(value);

            for (int i = 0; i < value; i++)
            {
                shopInventory.Add(new ShopSlot());
            }
        }
    }

    public ShopSystem(int size, int gold, float buyMarkup, float sellMarkup)
    {
        availableGold = gold;
        this.buyMarkup = buyMarkup;
        this.sellMarkup = sellMarkup;

        ShopSize = size;
    }

    private ShopSlot GetFreeSlot()
    {
        var freeSlot = shopInventory.FirstOrDefault(slot => slot.ItemData == null);

        if (freeSlot == null)
        {
            freeSlot = new ShopSlot();
            shopInventory.Add(freeSlot);
        }

        return freeSlot;
    }

    public bool ContainsItem(ItemData itemToCheck, out ShopSlot slots)
    {
        slots = shopInventory.Find(slot => slot.ItemData == itemToCheck);
        return slots != null;
    }

    public void AddToShop(ItemData itemToAdd, int amountToAdd)
    {
        if (ContainsItem(itemToAdd, out ShopSlot shopSlot))
        {
            shopSlot.AddToStack(amountToAdd);
        }

        var freeSlot = GetFreeSlot();
        freeSlot.AssignItem(itemToAdd, amountToAdd);
    }
}
