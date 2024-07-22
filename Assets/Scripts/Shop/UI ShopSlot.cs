using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIShopSlot : MonoBehaviour
{
    public ItemData itemData;
    public int price;

    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;

    private Button button;

    public ShopDisplay ParentDisplay { get; private set; }

    void Awake()
    {
        itemIcon.preserveAspect = true;

        button = GetComponent<Button>();
        button.onClick.AddListener(OnUISlotClick);

        ParentDisplay = GetComponentInParent<ShopDisplay>();
    }

    
    public void ClearUISlot()
    {
        itemData = null;
        price = -1;
        itemIcon.sprite = null;
        itemIcon.color = Color.clear;
        nameText.text = "";
        priceText.text = "";
    }

    public void UpdateUISlot(ItemData data, int price)
    {
        itemData = data;
        this.price = price;

        itemIcon.sprite = data.itemSprite;
        itemIcon.color = Color.white;
        nameText.text = data.itemName;
        priceText.text = price.ToString() + " G";
    }

    public void OnUISlotClick()
    {
        ParentDisplay.SlotClicked(this);
    }
}