using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActiveInventory : MonoBehaviour
{
    public InventoryData inventoryData;

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
    }

    public void Initialize()
    {
        for (int i = 0; i < InventoryData.maxActiveItems; i++)
        {
            var slot = Instantiate(activeInventorySlotPrefab, activeInventoryPanel.transform);
            UIInventorySlot slotScript = slot.GetComponent<UIInventorySlot>();
            if (inventoryData.activeItems[i] != null) {
                slotScript.SetItem(inventoryData.activeItems[i]);
            }
        }
        ChangeActiveHighlight(inventoryData.currentSlot);
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
        inventoryData.currentSlot = index;

        foreach (Transform inventorySlot in transform)
        {
            inventorySlot.GetChild(1).gameObject.SetActive(false);
        }

        transform.GetChild(inventoryData.currentSlot).GetChild(1).gameObject.SetActive(true);
    }

    private void Update()
    {
    }
}
