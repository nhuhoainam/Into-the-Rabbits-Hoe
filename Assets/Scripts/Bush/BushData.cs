using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BushData : ScriptableObject
{
    public float FruitSpawnChance = 0.5f;
    public Sprite BushWithFruitSprite;
    public Sprite BushSprite;
    public Sprite FruitSprite;
    public ItemData FruitItem;
}
