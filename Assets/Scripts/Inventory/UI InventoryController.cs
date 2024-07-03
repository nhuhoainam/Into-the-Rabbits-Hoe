using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryController : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel;

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
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(invToDisplay);
    }
}
