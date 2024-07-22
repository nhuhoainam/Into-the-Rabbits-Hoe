using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UniqueID))]
public class ShopKeeper : MonoBehaviour
{
    [SerializeField] private ShopItemList shopItemsHeld;
    public ShopSystem shopSystem;

    void Awake()
    {
        Debug.Log("Awake");
        shopSystem = new ShopSystem(
            shopItemsHeld.BuyMarkup,
            shopItemsHeld.SellMarkup
        );

        foreach (var item in shopItemsHeld.ShopItems)
        {
            Debug.Log($"Adding {item.itemName}: to shop");
            shopSystem.AddToShop(new ShopSlot(item));
        }
    }
}
