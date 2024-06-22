using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActiveInventory : MonoBehaviour
{
    public InventoryData inventoryData;
    public GameObject activeInventorySlotPrefab;
    public GameObject activeInventoryPanel;
    public List<UIInventorySlot> slots = new(InventoryData.maxActiveItems);


    private void Awake()
    {
        for (int i = 0; i < InventoryData.maxActiveItems; i++)
        {
            slots.Add(null);
        }
    }

    private void Start()
    {
        for (int i = 0; i < InventoryData.maxActiveItems; i++)
        {
            var slot = Instantiate(activeInventorySlotPrefab, activeInventoryPanel.transform);
            slots[i] = slot.transform.GetChild(0).GetComponent<UIInventorySlot>();
        }
        ChangeActiveHighlight(inventoryData.currentSlot);
        UpdateInventory();
    }

    public void ChangeActiveSlot(int slot)
    {
        ChangeActiveHighlight(slot - 1);
    }

    private void ChangeActiveHighlight(int index)
    {
        inventoryData.currentSlot = index;

        foreach (Transform inventorySlot in transform)
        {
            inventorySlot.GetChild(1).gameObject.SetActive(false);
        }

        transform.GetChild(inventoryData.currentSlot).GetChild(1).gameObject.SetActive(true);
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < InventoryData.maxActiveItems; i++)
        {
            if (inventoryData.activeItems[i] != null)
            {
                slots[i].SetItem(inventoryData.activeItems[i]);
            }
            else
            {
                slots[i].ClearItem();
            }
            slots[i].UpdateItemDisplay();
        }
        ChangeActiveHighlight(inventoryData.currentSlot);
    }
}
