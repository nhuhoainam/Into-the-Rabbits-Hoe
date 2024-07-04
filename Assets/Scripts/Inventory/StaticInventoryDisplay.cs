using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private UIInventorySlot[] slots;

    protected override void OnEnable() {
        base.OnEnable();
        PlayerInventoryHolder.OnPlayerInventoryChanged += RefreshDisplay;
    }

    protected override void OnDisable() {
        base.OnDisable();
        PlayerInventoryHolder.OnPlayerInventoryChanged -= RefreshDisplay;
    }

    void RefreshDisplay() {
        if (inventoryHolder != null) {
            inventorySystem = inventoryHolder.PrimaryInventorySystem;
            inventorySystem.OnInventorySlotChanged += UpdateSlot;
        }
        else {
            Debug.LogWarning("No Inventory Holder assigned to Static Inventory Display");
        }

        AssignSlot(inventorySystem);
    }

    protected override void Start()
    {
        base.Start();

        RefreshDisplay();
    }

    public override void AssignSlot(InventorySystem invToDisplay)
    {

        slotDictionary = new Dictionary<UIInventorySlot, InventorySlot>();

        if (slots.Length != inventorySystem.InventorySize) Debug.Log($"Inventory slots out of sync on {this.gameObject}");

        for (int i = 0; i < inventorySystem.InventorySize; i++)
        {
            slotDictionary.Add(slots[i], inventorySystem.InventorySlots[i]);
            slots[i].Init(inventorySystem.InventorySlots[i]);
        }
    }
}
