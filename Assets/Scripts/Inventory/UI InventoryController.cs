using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryController : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel; // For displaying the chests

    public DynamicInventoryDisplay playerInventoryPanel;

    private InventorySystem playerInventory;

    void Start()
    {
        playerInventory = GetComponent<PlayerInventoryHolder>().PrimaryInventorySystem;
    }

    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
        PlayerInventoryHolder.OnInventoryCloseRequested += CloseInventory;
        PlayerInventoryHolder.OnPlayerInventoryChanged += RefreshDisplay;
    }
    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
        PlayerInventoryHolder.OnInventoryCloseRequested -= CloseInventory;
        PlayerInventoryHolder.OnPlayerInventoryChanged -= RefreshDisplay;
    }

    void DisplayInventory(InventorySystem invToDisplay, int offset = 0)
    {
        if (inventoryPanel != null) {
            inventoryPanel.gameObject.SetActive(true);
            inventoryPanel.RefreshDynamicInventory(invToDisplay, offset);
        }
    }

    void RefreshDisplay()
    {
        if (playerInventoryPanel != null) {
            playerInventoryPanel.gameObject.SetActive(true);
            playerInventoryPanel.RefreshDynamicInventory(playerInventory, 9);
        }
    }

    public void CloseInventory()
    {
        if (inventoryPanel != null) inventoryPanel.gameObject.SetActive(false);
        playerInventoryPanel.gameObject.SetActive(false);
    }
}
