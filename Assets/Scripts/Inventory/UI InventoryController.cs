using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryController : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel;

    private bool isOpen = false;

    private void OnEnable()
    {

        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
    }
    private void OnDisable()
    {

        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
    }

    void Update() { }

    void DisplayInventory(InventorySystem invToDisplay)
    {
        if (!isOpen)
        {
            isOpen = true;
            inventoryPanel.gameObject.SetActive(true);
            inventoryPanel.RefreshDynamicInventory(invToDisplay);
        }
        else
        {
            isOpen = false;
            inventoryPanel.gameObject.SetActive(false);
        }
    }
}
