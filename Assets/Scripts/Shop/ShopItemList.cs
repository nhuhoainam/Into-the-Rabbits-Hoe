using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Shop Item List")]
public class ShopItemList : ScriptableObject
{
    [SerializeField] private List<ItemData> shopItems;
    [SerializeField] private float sellMarkup;
    [SerializeField] private float buyMarkup;

    public List<ItemData> ShopItems => shopItems;
    public float SellMarkup => sellMarkup;
    public float BuyMarkup => buyMarkup;
}