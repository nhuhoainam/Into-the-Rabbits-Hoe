using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private UIActiveInventorySlot[] slots;

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
            ChangeActiveSlot(0);
        }
        else {
            Debug.LogWarning("No Inventory Holder assigned to Static Inventory Display");
        }

        AssignSlot(inventorySystem);
    }

    protected override void Start()
    {
        base.Start();

        playerControls.Inventory.Active.performed += ctx => ChangeActiveSlot((int)ctx.ReadValue<float>() - 1);
        ChangeActiveSlot(0);

        RefreshDisplay();
    }

    void ChangeActiveSlot(int slot) {
        inventoryHolder.ActiveSlot = slot;

        foreach (var slotObj in slots) {
            slotObj.ActiveSlotIndicator.SetActive(false);
        }
        slots[slot].ActiveSlotIndicator.SetActive(true);
    }

    public override void AssignSlot(InventorySystem invToDisplay, int offset = 0)
    {
        Debug.Log("Assigning slots");
        slotDictionary = new Dictionary<UIInventorySlot, InventorySlot>();

        if (slots.Length != inventoryHolder.Offset) Debug.Log($"Inventory slots out of sync on {gameObject}");

        for (int i = 0; i < inventoryHolder.Offset; i++)
        {
            slotDictionary.Add(slots[i].slot, inventorySystem.InventorySlots[i]);
            slots[i].slot.Init(inventorySystem.InventorySlots[i]);
        }
    }
}
