using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopDisplay : MonoBehaviour
{
    [SerializeField] private MouseItemData mouseItem;
    [SerializeField] private UIShopSlot itemListingPrefab;

    [SerializeField] private ShopSystem shopSystem;
    private Dictionary<UIShopSlot, ShopSlot> slotDictionary;

    public ShopSystem ShopSystem
    {
        get => shopSystem;
        set => shopSystem = value;
    }
    public Dictionary<UIShopSlot, ShopSlot> SlotDictionary => slotDictionary;

    [SerializeField] private PlayerController player;
    private PlayerData playerData;

    void Awake()
    {
        playerData = player.playerData;
    }

    void Start()
    {
        AssignSlots();
    }

    public void AssignSlots()
    {
        ClearSlots();

        slotDictionary = new Dictionary<UIShopSlot, ShopSlot>();

        for (int i = 0; i < shopSystem.ShopSize; i++)
        {
            var slot = Instantiate(itemListingPrefab, transform);
            slotDictionary.Add(slot.GetComponent<UIShopSlot>(), shopSystem.shopInventory[i]);
            slot.UpdateUISlot(
                shopSystem.shopInventory[i].item,
                shopSystem.shopInventory[i].item.goldValue
                    + (int)(shopSystem.shopInventory[i].item.goldValue * shopSystem.buyMarkup)
            );
        }
    }

    public void ClearSlots()
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