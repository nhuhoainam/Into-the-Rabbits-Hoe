using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public ItemData item;
    public int amount;

    void Awake()
    {
        SaveGameManager.OnSaveGame += SaveItem;
    }

    private void SaveItem()
    {
        SaveGameManager.CurrentSaveData.droppedItems.Add(new DroppedItemSaveData(item.itemID, amount, transform.position));
    }

    public void SetItem(ItemData item, int amount)
    {
        this.item = item;
        this.amount = amount;
        GetComponentInChildren<SpriteRenderer>().sprite = item.itemSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerInventoryHolder inventoryHolder))
        {
            if (inventoryHolder.PrimaryInventorySystem.AddToInventory(item, amount))
            {
                Debug.Log("Item added to inventory");
                Destroy(gameObject);
            }
        }
    }

    void OnDestroy()
    {
        SaveGameManager.OnSaveGame -= SaveItem;
    }
}

[System.Serializable]
public class DroppedItemSaveData
{
    public int itemID;
    public int amount;
    public Vector3 position;

    public DroppedItemSaveData(int itemID, int amount, Vector3 position)
    {
        this.itemID = itemID;
        this.amount = amount;
        this.position = position;
    }
}