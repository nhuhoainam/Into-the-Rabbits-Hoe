using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] private List<ItemData> itemDatabase = new();

    [ContextMenu("Set IDs")]
    public void SetItemIDs()
    {
        itemDatabase = new List<ItemData>();

        var foundItems = Resources.LoadAll<ItemData>(path: "ItemData").OrderBy(i => i.itemID).ToList();

        var hasIDInRange = foundItems.Where(i => i.itemID != -1 && i.itemID < foundItems.Count).OrderBy(i => i.itemID).ToList();
        var hasIDNotInRange = foundItems.Where(i => i.itemID != -1 && i.itemID >= foundItems.Count).OrderBy(i => i.itemID).ToList();
        var noID = foundItems.Where(i => i.itemID <= -1).ToList();

        var index = 0;
        for (int i = 0; i < foundItems.Count; i++)
        {
            ItemData itemToAdd;
            itemToAdd = hasIDInRange.Find(d => d.itemID == i);

            if (itemToAdd != null)
            {
                itemDatabase.Add(itemToAdd);
                continue;
            }
            if (index < noID.Count)
            {
                noID[index].itemID = i;
                itemToAdd = noID[index];
                index++;
                itemDatabase.Add(itemToAdd);
            }
        }

        foreach (var item in hasIDNotInRange)
        {
            itemDatabase.Add(item);
        }
    }

    public ItemData GetItem(int id)
    {
        return itemDatabase.Find(i => i.itemID == id);
    }
}
