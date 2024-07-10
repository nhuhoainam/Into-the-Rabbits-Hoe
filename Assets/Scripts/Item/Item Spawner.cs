using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : Singleton<ItemSpawner>
{
    public Database itemDatabase;
    public GameObject itemContainerPrefab;

    protected override void Awake()
    {
        SaveGameManager.OnLoadGame += LoadDroppedItems;
    }

    private void LoadDroppedItems(SaveData data)
    {
        if (data.droppedItems == null) return;
        foreach (var droppedItem in data.droppedItems)
        {
            SpawnItem(droppedItem.itemID, droppedItem.position, droppedItem.amount);
        }
    }

    public void SpawnItem(int id, Vector3 position, int amount = 1)
    {
        ItemData item = itemDatabase.GetItem(id);
        var itemContainer = Instantiate(itemContainerPrefab, position, Quaternion.identity).GetComponent<ItemContainer>();
        itemContainer.SetItem(item, amount);
        StartCoroutine(
            // disable the collider for a short time to prevent the item from being picked up immediately
            DisableCollider(itemContainer.GetComponent<Collider2D>(), 1.0f)
        );
    }

    private IEnumerator DisableCollider(Collider2D collider, float time)
    {
        collider.enabled = false;
        yield return new WaitForSeconds(time);
        collider.enabled = true;
    }
}
