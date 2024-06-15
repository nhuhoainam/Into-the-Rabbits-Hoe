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

    public void Initialize() {
        for (int i = 0; i < InventoryData.maxNoItems; i++) {
            UIInventorySlot slotScript = inventoryPanel.transform.GetChild(i).GetComponent<UIInventorySlot>();
            if (inventoryData.items[i] != null) {
                slotScript.SetItem(inventoryData.items[i]);
            }
        }
    }

    private void ToggleInventory()
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
        Initialize();
    }

    private void CloseInventory()
    {
        Debug.Log("Closing inventory");
        isOpen = false;
        inventoryPanel.SetActive(false);
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
            Instantiate(inventorySlotPrefab, inventoryPanel.transform);
        }
    }
}
