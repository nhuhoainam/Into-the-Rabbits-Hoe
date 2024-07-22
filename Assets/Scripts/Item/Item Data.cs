using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Item Data")]
public class ItemData : ScriptableObject
{
    public int itemID = -1;
    public string itemName;
    public Sprite itemSprite;
    public bool isStackable;
    public uint maxStackSize;
    [TextArea]
    public string itemDescription;
    public int goldValue;
}
