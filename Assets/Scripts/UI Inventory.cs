using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public InventoryData inventoryData;
    public GameObject inventorySlotPrefab;
    public GameObject inventoryPanel;

    private PlayerControls playerControls;

    public bool isOpen = false;

    void Awake() {
        playerControls = new PlayerControls();
    }

    private void ToggleInventory()
    {
        Debug.Log("Toggling inventory");
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);
    }

    void OnEnable() {
        playerControls.Enable();
    }

    void OnDisable() {
        playerControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerControls.Inventory.OpenInventory.performed += ctx => ToggleInventory();

        for (int i = 0; i < InventoryData.maxNoItems; i++) {
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryPanel.transform);
            UIInventorySlot slotScript = slot.GetComponent<UIInventorySlot>();
            if (inventoryData.items[i] != null) {
                slotScript.SetItem(inventoryData.items[i]);
            }
        }
    }
}
