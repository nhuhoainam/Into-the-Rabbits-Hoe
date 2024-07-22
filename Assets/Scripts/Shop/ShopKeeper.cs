using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UniqueID))]
public class ShopKeeper : MonoBehaviour, IPlayerInteractable
{
    [SerializeField] private ShopItemList shopItemsHeld;
    private ShopSystem shopSystem;

    void Awake()
    {
        shopSystem = new ShopSystem(
            shopItemsHeld.BuyMarkup,
            shopItemsHeld.SellMarkup
        );

        foreach (var item in shopItemsHeld.ShopItems)
        {
            Debug.Log($"Adding {item.itemName}: to shop");
            shopSystem.AddToShop(item);
        }
    }

    public void Interact(PlayerData playerData)
    {
        throw new System.NotImplementedException();
    }
}
