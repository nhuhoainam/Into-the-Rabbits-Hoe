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

    void Awake()
    {
        for (int i = 0; i < InventoryData.maxNoItems; i++)
        {
            var slot = Instantiate(inventorySlotPrefab, inventoryPanel.transform);
            slots.Add(slot.GetComponent<UIInventorySlot>());
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
            slots[i].UpdateItemDisplay();
        }
    }
}
