using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    [TextArea]
    public GameObject model;
    public string itemDescription;
}
