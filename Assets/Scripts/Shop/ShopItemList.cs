using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Shop Item List")]
public class ShopItemList : ScriptableObject
{
    [SerializeField] private List<ShopItem> shopItems;
    [SerializeField] private int maxAllowedGold;
    [SerializeField] private float sellMarkup;
    [SerializeField] private float buyMarkup;

    public List<ShopItem> ShopItems => shopItems;
    public int MaxAllowedGold => maxAllowedGold;
    public float SellMarkup => sellMarkup;
    public float BuyMarkup => buyMarkup;
}

[System.Serializable]
public struct ShopItem
{
    public ItemData itemData;
    public int amount;
}
