using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public int itemID = -1;
    public string itemName;
    public Sprite itemSprite;
    public bool isStackable;
    public uint maxStackSize;
    [TextArea]
    public string itemDescription;
    public GameObject itemPrefab; // for dropping items
}
