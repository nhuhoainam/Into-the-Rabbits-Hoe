using System;
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

    private void SaveInventory()
    {
        // SaveGameManager.CurrentSaveData.playerInventory = new InventorySaveData(primaryInventorySystem);
    }

    protected override void LoadInventory(SaveData data)
    {
        if (data.playerInventory.InvSystem != null)
        {
            primaryInventorySystem = data.playerInventory.InvSystem;
            OnPlayerInventoryChanged.Invoke();
        }
    }

    void Start()
    {
        playerControls.Inventory.OpenInventory.performed += ctx => OnDynamicInventoryDisplayRequested?.Invoke(primaryInventorySystem, offset);
        playerControls.Inventory.CloseInventory.performed += ctx => OnInventoryCloseRequested?.Invoke();

        SaveGameManager.CurrentSaveData.playerInventory = new InventorySaveData(primaryInventorySystem);
    }

    private void OnEnable()
    {
        playerControls.Enable();
        SaveGameManager.OnLoadGame += LoadInventory;
        SaveGameManager.OnSaveGame += SaveInventory;
    }

    private void OnDisable()
    {
        playerControls.Disable();
        SaveGameManager.OnLoadGame -= LoadInventory;
        SaveGameManager.OnSaveGame -= SaveInventory;
    }

    public bool AddToInventory(ItemData item, int amount)
    {
        if (primaryInventorySystem.AddToInventory(item, amount)) return true;
        return false;
    }

    public bool ContainsItem(ItemData item, out List<InventorySlot> slots)
    {
        return primaryInventorySystem.ContainsItem(item, out slots);
    }

    public bool ContainsItems(ItemData item, int amount)
    {
        return primaryInventorySystem.ContainsItems(item, amount);
    }
}
