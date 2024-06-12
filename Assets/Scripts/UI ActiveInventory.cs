using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActiveInventory : MonoBehaviour
{
    public InventoryData activeInventoryData;

    public GameObject activeInventorySlotPrefab;
    public GameObject activeInventoryPanel;

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        playerControls.Inventory.Active.performed += ctx => ChangeActiveSlot((int)ctx.ReadValue<float>());

        for (int i = 0; i < InventoryData.maxActiveItems; i++)
        {
            var slot = Instantiate(activeInventorySlotPrefab, activeInventoryPanel.transform);
            UIInventorySlot slotScript = slot.GetComponent<UIInventorySlot>();
            if (activeInventoryData.items[i] != null) {
                slotScript.SetItem(activeInventoryData.items[i]);
            }
        }
        ChangeActiveHighlight(activeInventoryData.currentSlot);
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void ChangeActiveSlot(int slot)
    {
        ChangeActiveHighlight(slot - 1);
    }

    private void ChangeActiveHighlight(int index)
    {
        activeInventoryData.currentSlot = index;

        foreach (Transform inventorySlot in transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        transform.GetChild(activeInventoryData.currentSlot).GetChild(0).gameObject.SetActive(true);
    }
}
