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
        base.Awake();
        SaveGameManager.OnLoadScene += LoadDroppedItems;
    }

    void OnDestroy()
    {
        SaveGameManager.OnLoadScene -= LoadDroppedItems;
    }

    private void LoadDroppedItems(SaveData data, int sceneIndex)
    {
        var droppedItems = data.sceneData[sceneIndex].droppedItems;
        if (droppedItems == null) return;
        foreach (var droppedItem in droppedItems)
        {
            SpawnItem(droppedItem.itemID, droppedItem.position, droppedItem.amount);
        }
    }

    public void SpawnItem(string name, Vector3 position, int amount = 1)
    {
        ItemData item = itemDatabase.GetItem(name);
        SpawnItem(item, position, amount);
    }

    public void SpawnItem(int id, Vector3 position, int amount = 1)
    {
        ItemData item = itemDatabase.GetItem(id);
        SpawnItem(item, position, amount);
    }

    void SpawnItem(ItemData item, Vector3 position, int amount)
    {
        var itemContainer = Instantiate(itemContainerPrefab, position, Quaternion.identity).GetComponent<ItemContainer>();
        itemContainer.SetItem(item, amount);
        StartCoroutine(
            // disable the collider for a short time to prevent the item from being picked up immediately
            DisableCollider(itemContainer.gameObject.GetComponent<Collider2D>(), 0.5f)
        );
    }


    private IEnumerator DisableCollider(Collider2D collider, float time)
    {
        collider.enabled = false;
        yield return new WaitForSeconds(time);
        collider.enabled = true;
    }
}
