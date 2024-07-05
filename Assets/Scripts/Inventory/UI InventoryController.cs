using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryController : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel; // For displaying the chests

    public DynamicInventoryDisplay playerInventoryPanel;

    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested += DisplayPlayerInventory;
        PlayerInventoryHolder.OnInventoryCloseRequested += CloseInventory;
    }
    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested -= DisplayPlayerInventory;
        PlayerInventoryHolder.OnInventoryCloseRequested -= CloseInventory;
    }

    void DisplayInventory(InventorySystem invToDisplay)
    {
        if (inventoryPanel != null) {
            inventoryPanel.gameObject.SetActive(true);
            inventoryPanel.RefreshDynamicInventory(invToDisplay);
        }
    }

    void DisplayPlayerInventory(InventorySystem invToDisplay)
    {
        playerInventoryPanel.gameObject.SetActive(true);
        playerInventoryPanel.RefreshDynamicInventory(invToDisplay);
    }

    public void CloseInventory()
    {
        if (inventoryPanel != null) inventoryPanel.gameObject.SetActive(false);
        playerInventoryPanel.gameObject.SetActive(false);
    }
}
