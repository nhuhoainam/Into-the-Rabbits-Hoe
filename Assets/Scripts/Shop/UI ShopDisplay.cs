using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopDisplay : MonoBehaviour
{
    [SerializeField] private MouseItemData mouseItem;
    [SerializeField] private UIShopSlot itemListingPrefab;
    [SerializeField] private PlayerData playerData;

    [SerializeField] private ShopSystem shopSystem;
    private Dictionary<UIShopSlot, ItemData> slotDictionary;

    public ShopSystem ShopSystem => shopSystem;
    public Dictionary<UIShopSlot, ItemData> SlotDictionary => slotDictionary;

    void Start()
    {
        AssignSlots();
    }

    public void AssignSlots()
    {
        ClearSlots();

        slotDictionary = new Dictionary<UIShopSlot, ItemData>();

        for (int i = 0; i < shopSystem.ShopSize; i++)
        {
            var slot = Instantiate(itemListingPrefab, transform);
            slotDictionary.Add(slot.GetComponent<UIShopSlot>(), shopSystem.shopInventory[i]);
            slot.UpdateUISlot(shopSystem.shopInventory[i], shopSystem.shopInventory[i].goldValue + (int)(shopSystem.shopInventory[i].goldValue * shopSystem.buyMarkup));
        }
    }

    private void ClearSlots()
    {
        foreach (var item in transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }

        slotDictionary?.Clear();
    }

    public void SlotClicked(UIShopSlot clickedSlot)
    {
        if (clickedSlot.itemData == null) return;
        if (playerData.money < clickedSlot.price) return;
        // Clicked with item in mouse 
        // Check if mouse item is the same as the clicked item
        if (mouseItem.AssignedSlot.ItemData != null && mouseItem.AssignedSlot.ItemData != clickedSlot.itemData) return;
        if (mouseItem.AssignedSlot.ItemData != null)
        {
            // Check if there is enough space in the mouse item slot
            if (mouseItem.AssignedSlot.StackSize >= mouseItem.AssignedSlot.ItemData.maxStackSize) return;

            // Add item to mouse item slot
            mouseItem.AssignedSlot.AddToStack(1);
            // Remove money from player
            playerData.money -= clickedSlot.price;
        }
        else
        {
            // Add item to mouse item slot
            mouseItem.UpdateMouseSlot(new InventorySlot(clickedSlot.itemData, 1));
            // Remove money from player
            playerData.money -= clickedSlot.price;
        }
    }
}