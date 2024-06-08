using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstance : MonoBehaviour
{
    public ItemData itemData;
    // public int quantity = 1;

    public ItemInstance(ItemData itemData)
    {
        this.itemData = itemData;
    }
}
