using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour
{
    public ItemInstance itemInstance;
    private Image itemDisplay;

    void Awake() {
        itemInstance = null;
        itemDisplay = transform.GetChild(0).GetComponent<Image>();
        // Debug.Log("ItemDisplay: " + itemDisplay);
    }

    public void Start() {
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

    public void Update() {
        if (itemInstance != null) {
            itemDisplay.gameObject.SetActive(true);
        }
        else {
            itemDisplay.gameObject.SetActive(false);
        }
    }
}
