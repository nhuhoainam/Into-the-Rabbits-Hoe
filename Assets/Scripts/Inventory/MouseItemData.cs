using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MouseItemData : MonoBehaviour
{
    public Image itemSprite;
    public TextMeshProUGUI amount;
    public InventorySlot AssignedSlot;

    void Awake()
    {
        itemSprite = GetComponent<Image>();
        amount = GetComponentInChildren<TextMeshProUGUI>();
        itemSprite.color = Color.clear;
        amount.text = "";
    }

    public void UpdateMouseSlot(InventorySlot slot)
    {
        AssignedSlot.AssignItem(slot);
        itemSprite.sprite = slot.ItemData.itemSprite;
        itemSprite.color = Color.white;
        if (slot.StackSize > 1) amount.text = slot.StackSize.ToString();
        else amount.text = "";
    }

    void Update()
    {
        if (AssignedSlot.ItemData != null) {
            transform.position = Mouse.current.position.ReadValue();

            if (Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject())
            {
                ClearSlot();
            }
        }
    }

    public void ClearSlot()
    {
        AssignedSlot.ClearSlot();
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        amount.text = "";
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new(EventSystem.current)
        {
            position = Mouse.current.position.ReadValue()
        };
        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}
