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


    public void RefreshDynamicInventory(InventorySystem invToDisplay, int offset)
    {
        ClearSlots();
        inventorySystem = invToDisplay;
        AssignSlot(invToDisplay, offset);
    }

    public override void AssignSlot(InventorySystem invToDisplay, int offset)
    {
        ClearSlots();

        slotDictionary = new Dictionary<UIInventorySlot, InventorySlot>();

        if (invToDisplay == null) return;

        for (int i = offset; i < invToDisplay.InventorySize; i++)
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
