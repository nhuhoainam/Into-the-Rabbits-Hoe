using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour
{
    public ItemInstance itemInstance;
    private Image itemDisplay;
    private TextMeshProUGUI itemQuantityDisplay;

    void Awake() {
        itemInstance = null;
        itemDisplay = transform.GetChild(0).GetComponent<Image>();
        itemQuantityDisplay = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        // Debug.Log("ItemDisplay: " + itemDisplay);
    }

    public void SetItem(ItemInstance newItemInstance)
    {
        itemInstance = newItemInstance;
        Debug.Log("Setting item: " + itemInstance.itemData.itemSprite.name);
        itemDisplay.sprite = itemInstance.itemData.itemSprite;
        // itemDisplay.sprite = Sprite.Create(itemInstance.itemData.itemSprite.texture, new Rect(0, 0, itemInstance.itemData.itemSprite.texture.width, itemInstance.itemData.itemSprite.texture.height), new Vector2(0.5f, 0.5f));
    }

    public void ClearItem()
    {
        itemInstance = null;
        itemDisplay.sprite = null;
    }

    public void UpdateItemDisplay()
    {
        if (itemInstance != null)
        {
            itemDisplay.sprite = itemInstance.itemData.itemSprite;
            itemDisplay.gameObject.SetActive(true);
            if (itemInstance.quantity > 1)
            {
                itemQuantityDisplay.text = itemInstance.quantity.ToString();
                itemQuantityDisplay.gameObject.SetActive(true);
            }
            else
            {
                itemQuantityDisplay.text = "";
                itemQuantityDisplay.gameObject.SetActive(true);
            }
        }
        else
        {
            itemDisplay.sprite = null;
            itemDisplay.gameObject.SetActive(false);
            itemQuantityDisplay.text = "";
            itemQuantityDisplay.gameObject.SetActive(false);
        }
    }
}
