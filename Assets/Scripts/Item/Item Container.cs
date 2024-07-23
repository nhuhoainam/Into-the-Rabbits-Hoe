using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public ItemData item;
    public int amount;

    void Start()
    {
        if (item != null)
        {
            SetItem(item, amount);
        }
    }

    void Awake()
    {
        SaveGameManager.OnSaveScene += SaveItem;
        DontDestroyOnLoad(gameObject);
    }

    private void SaveItem(int sceneIndex)
    {
        Debug.Log("Saving item: " + item.itemName + " at position: " + transform.position);
        SaveGameManager.CurrentSaveData.sceneData[sceneIndex].droppedItems.Add(new DroppedItemSaveData(item.itemID, amount, transform.position));
        Destroy(gameObject);
    }

    public void SetItem(ItemData item, int amount)
    {
        this.item = item;
        this.amount = amount;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = item.itemSprite;
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
        SaveGameManager.OnSaveScene -= SaveItem;
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