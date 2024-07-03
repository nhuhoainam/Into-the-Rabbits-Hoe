using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;

    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(ItemData itemToAdd, int amountToAdd)
    {

        if (ContainsItem(itemToAdd, out List<InventorySlot> invSlot)) // Check whether item exists in inventory.
        {
            foreach (InventorySlot slot in invSlot)
            {
                if (slot.EnoughRoomInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
            return true;
        }
        else if (HasFreeSlot(out InventorySlot freeSlot)) // Gets the first available slot
        {
            if (freeSlot.EnoughRoomInStack(amountToAdd))
            {
                freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
                OnInventorySlotChanged?.Invoke(freeSlot);
                return true;
            }
        }
        return false;
    }

    public bool ContainsItem(ItemData itemToCheck, out List<InventorySlot> slots)
    {
        slots = inventorySlots.Where(slot => slot.ItemData == itemToCheck).ToList();
        return slots != null && slots.Count > 0;
    }

    public bool HasFreeSlot(out InventorySlot freeSlots)
    {
        freeSlots = inventorySlots.FirstOrDefault(slot => slot.ItemData == null);
        return freeSlots != null;
    }
}
