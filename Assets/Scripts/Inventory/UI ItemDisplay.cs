using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemDisplay : MonoBehaviour
{
    public ItemInstance itemInstance;
    private Image itemImage;
    private TextMeshProUGUI itemQuantityDisplay;

    void Awake()
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();
        itemQuantityDisplay = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void SetItem(ItemInstance newItemInstance)
    {
        itemInstance = newItemInstance;
        itemImage.sprite = itemInstance.itemData.itemSprite;
        itemImage.gameObject.SetActive(true);
        if (itemInstance.quantity > 1)
        {
            itemQuantityDisplay.text = itemInstance.quantity.ToString();
            itemQuantityDisplay.gameObject.SetActive(true);
        }
        else
        {
            itemQuantityDisplay.text = "";
            itemQuantityDisplay.gameObject.SetActive(false);
        }
    }
}
