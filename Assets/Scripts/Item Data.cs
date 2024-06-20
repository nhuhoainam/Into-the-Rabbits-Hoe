using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public int itemID;
    public string itemName;
    public Sprite itemSprite;
    public bool isStackable;
    public uint maxStackSize;
    [TextArea]
    public string itemDescription;
}
