using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInventoryHolder : InventoryHolder
{
    [SerializeField] protected int secondaryInventorySize;
    [SerializeField] protected InventorySystem secondaryInventorySystem;

    public static UnityAction OnPlayerInventoryChanged;
    public static UnityAction<InventorySystem> OnPlayerInventoryDisplayRequested;
    public static UnityAction OnInventoryCloseRequested;

    public InventorySystem SecondaryInventorySystem => secondaryInventorySystem;

    private PlayerControls playerControls;

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
        secondaryInventorySystem = new InventorySystem(secondaryInventorySize);
    }

    void Start()
    {
        playerControls.Inventory.OpenInventory.performed += ctx => OnPlayerInventoryDisplayRequested?.Invoke(secondaryInventorySystem);
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
        else if (secondaryInventorySystem.AddToInventory(item, amount)) return true;
        else return false;
    }
}
