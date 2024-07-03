using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainInventory : MonoBehaviour
{
    public InventoryData inventoryData;
    public GameObject inventorySlotPrefab;
    public GameObject inventoryPanel;
    public List<UIInventorySlot> slots = new(InventoryData.maxNoItems);

    public UIInventorySlot.OnMouseDrop onMouseDrop;

    void Awake()
    {
        for (int i = 0; i < InventoryData.maxNoItems; i++)
        {
            var slot = Instantiate(inventorySlotPrefab, inventoryPanel.transform);
            slots.Add(slot.GetComponent<UIInventorySlot>());
            slots[i].slotIndex = i;
            slots[i].isActiveSlot = false;
            slots[i].onMouseDrop += onMouseDrop;
        }
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < InventoryData.maxNoItems; i++)
        {
            if (inventoryData.items[i] != null)
            {
                slots[i].SetItem(inventoryData.items[i]);
            }
            else
            {
                slots[i].ClearItem();
            }
        }
    }
}
