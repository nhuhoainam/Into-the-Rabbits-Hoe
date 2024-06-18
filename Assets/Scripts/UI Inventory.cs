using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public InventoryData inventoryData;
    public GameObject inventorySlotPrefab;
    public GameObject inventoryPanel;
    public List<UIInventorySlot> slots = new(InventoryData.maxNoItems);


    public bool isOpen = false;

    void Awake()
    {

        for (int i = 0; i < InventoryData.maxNoItems; i++)
        {
            slots.Add(null);
        }
    }

    public void ToggleInventory()
    {
        if (isOpen)
        {
            CloseInventory();
        }
        else
        {
            OpenInventory();
        }
    }

    private void OpenInventory()
    {
        Debug.Log("Opening inventory");
        isOpen = true;
        inventoryPanel.SetActive(true);
        UpdateInventory();
    }

    private void CloseInventory()
    {
        Debug.Log("Closing inventory");
        isOpen = false;
        inventoryPanel.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < InventoryData.maxNoItems; i++)
        {
            var slot = Instantiate(inventorySlotPrefab, inventoryPanel.transform);
            slots[i] = slot.GetComponent<UIInventorySlot>();
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
