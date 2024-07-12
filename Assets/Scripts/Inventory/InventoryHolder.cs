using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public abstract class InventoryHolder : MonoBehaviour
{
    [SerializeField] protected int primaryInventorySize = 45;
    [SerializeField] protected InventorySystem primaryInventorySystem;
    [SerializeField] protected int offset = 9;

    [Range(0, 8)]
    public int ActiveSlot = 0;

    public int Offset => offset;

    public InventorySystem PrimaryInventorySystem => primaryInventorySystem;

    public static UnityAction<InventorySystem, int> OnDynamicInventoryDisplayRequested;

    protected virtual void Awake()
    {
        primaryInventorySystem = new(primaryInventorySize);

        SaveGameManager.OnLoadGame += LoadInventory;
    }

    protected abstract void LoadInventory(SaveData data);

    public InventorySlot GetItemInActiveSlot()
    {
        return primaryInventorySystem.InventorySlots[ActiveSlot];
    }
}

[System.Serializable]
public class InventorySaveData
{
    public InventorySystem InvSystem;

    public InventorySaveData(InventorySystem invSystem)
    {
        InvSystem = invSystem;
    }
}