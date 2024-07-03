using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private InventorySlot assignedInventorySlot;

    private Button button;

    public InventorySlot AssignedSlot => assignedInventorySlot;

    public InventoryDisplay ParentDisplay { get; private set; }

    private void Awake()
    {
        ClearUISlot();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnUISlotClick);

        ParentDisplay = GetComponentInParent<InventoryDisplay>();
    }

    public void Init(InventorySlot slot)
    {
        assignedInventorySlot = slot;
        UpdateUISlot();
    }

    public void UpdateUISlot(InventorySlot slot)
    {
        assignedInventorySlot = slot;
        if (slot.ItemData != null)
        {
            itemSprite.sprite = slot.ItemData.itemSprite;
            itemSprite.color = Color.white;

            if (slot.StackSize > 1) itemCount.text = slot.StackSize.ToString();
            else itemCount.text = "";
        }
        else
        {
            itemSprite.sprite = null;
            itemSprite.color = Color.clear;
            itemCount.text = "";
        }

    }

    public void UpdateUISlot()
    {
        if (assignedInventorySlot != null) UpdateUISlot(assignedInventorySlot);
    }

    public void ClearUISlot()
    {
        assignedInventorySlot?.ClearSlot();
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }

    public void OnUISlotClick()
    {
        ParentDisplay.SlotClicked(this);
    }
}
