using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInventoryHolder : InventoryHolder
{
    public static UnityAction OnPlayerInventoryChanged;
    public static UnityAction<InventorySystem> OnPlayerInventoryDisplayRequested;
    public static UnityAction OnInventoryCloseRequested;

    private PlayerControls playerControls;

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
    }

    protected override void LoadInventory(SaveData data)
    {
        if (data.playerInventory.InvSystem != null)
        {
            primaryInventorySystem = data.playerInventory.InvSystem;
            OnPlayerInventoryChanged?.Invoke();
        }
    }

    void Start()
    {
        playerControls.Inventory.OpenInventory.performed += ctx => OnDynamicInventoryDisplayRequested?.Invoke(primaryInventorySystem, offset);
        playerControls.Inventory.CloseInventory.performed += ctx => OnInventoryCloseRequested?.Invoke();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public bool AddToInventory(ItemData item, int amount)
    {
        if (primaryInventorySystem.AddToInventory(item, amount)) return true;
        return false;
    }
}
