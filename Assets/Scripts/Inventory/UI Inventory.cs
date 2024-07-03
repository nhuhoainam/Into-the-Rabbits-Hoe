using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public InventoryData inventoryData;
    public GameObject uiMainInventoryRef;
    public GameObject uiActiveInventoryRef;
    private UIMainInventory uiMainInventory;
    private UIActiveInventory uiActiveInventory;

    public PlayerControls playerControls;

    bool isOpen = false;

    void Awake()
    {
        playerControls = new PlayerControls();
    }

    void OnEnable()
    {
        playerControls.Enable();
    }

    void OnDisable()
    {
        playerControls.Disable();
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
        uiMainInventoryRef.SetActive(true);
        uiMainInventory.UpdateInventory();
    }

    private void CloseInventory()
    {
        Debug.Log("Closing inventory");
        isOpen = false;
        uiMainInventoryRef.SetActive(false);
    }

    public void Init()
    {
        uiMainInventory = uiMainInventoryRef.GetComponent<UIMainInventory>();
        uiActiveInventory = uiActiveInventoryRef.GetComponent<UIActiveInventory>();
        uiMainInventory.inventoryData = inventoryData;
        uiActiveInventory.inventoryData = inventoryData;
        uiMainInventory.onMouseDrop = OnMouseDrop;
        uiActiveInventory.onMouseDrop = OnMouseDrop;
    }

    void Start()
    {
        playerControls.Inventory.Active.performed += ctx => uiActiveInventory.ChangeActiveSlot((int)ctx.ReadValue<float>());
        playerControls.Inventory.OpenInventory.performed += ctx => ToggleInventory();
    }

    public void UpdateInventory()
    {
        if (isOpen == true)
        {
            uiMainInventory.UpdateInventory();
        }
        uiActiveInventory.UpdateInventory();
    }

    public void UpdateInventory(bool active)
    {
        if (active)
        {
            uiActiveInventory.UpdateInventory();
            return;
        }
        if (isOpen)
        {
            uiMainInventory.UpdateInventory();
        }
    }

    public void OnMouseDrop(GameObject dropped, int slotIndex, bool isActiveSlot)
    {
        var item = dropped.GetComponent<DraggableItem>();
        var oldSlotIndex = item.slotIndex;
        var oldIsActiveSlot = item.isActiveSlot;
        if (isActiveSlot == oldIsActiveSlot && slotIndex == oldSlotIndex)
        {
            return;
        }
        if (oldIsActiveSlot)
        {
            inventoryData.activeItems[oldSlotIndex] = null;
        }
        else
        {
            inventoryData.items[oldSlotIndex] = null;
        }
        if (isActiveSlot)
        {
            inventoryData.AddActiveItem(item.itemInstance, slotIndex);
        }
        else
        {
            inventoryData.AddItem(item.itemInstance, slotIndex);
        }
        UpdateInventory();
    }
}
