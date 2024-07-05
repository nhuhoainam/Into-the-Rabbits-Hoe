using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DynamicInventoryDisplay : InventoryDisplay
{
    [SerializeField] protected UIInventorySlot inventorySlotPrefab;

    protected override void Start()
    {
        base.Start();
    }


    public void RefreshDynamicInventory(InventorySystem invToDisplay)
    {
        ClearSlots();
        inventorySystem = invToDisplay;
        AssignSlot(invToDisplay);
    }

    public override void AssignSlot(InventorySystem invToDisplay)
    {
        ClearSlots();

        slotDictionary = new Dictionary<UIInventorySlot, InventorySlot>();

        if (invToDisplay == null) return;

        for (int i = 0; i < invToDisplay.InventorySize; i++)
        {
            var slot = Instantiate(inventorySlotPrefab, transform);
            slotDictionary.Add(slot, invToDisplay.InventorySlots[i]);
            slot.Init(invToDisplay.InventorySlots[i]);
            slot.UpdateUISlot();
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
}