using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;

    protected InventorySystem inventorySystem;
    protected Dictionary<UIInventorySlot, InventorySlot> slotDictionary;
    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<UIInventorySlot, InventorySlot> SlotDictionary => slotDictionary;

    private PlayerControls playerControls;
    private bool isSplittingStack = false;

    void Awake()
    {
        playerControls = new PlayerControls();
    }

    protected virtual void OnEnable()
    {
        playerControls.Inventory.Enable();
    }

    protected virtual void OnDisable()
    {
        playerControls.Inventory.Disable();
    }

    protected virtual void Start()
    {
        playerControls.Inventory.SplitStack.performed += ctx => isSplittingStack = true;
        playerControls.Inventory.SplitStack.canceled += ctx => isSplittingStack = false;
    }

    public abstract void AssignSlot(InventorySystem invToDisplay);

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach (var slot in SlotDictionary)
        {
            if (slot.Value == updatedSlot)
                slot.Key.UpdateUISlot(updatedSlot);
        }
    }

    public void SlotClicked(UIInventorySlot clickedUISlot)
    {
        Debug.Log("Slot clicked");
        if (Keyboard.current.shiftKey.isPressed) Debug.Log("Splitting stack");
        // Clicked slot has item & mouse doesn't have item => pick up item.
        if (clickedUISlot.AssignedSlot.ItemData != null && mouseInventoryItem.AssignedSlot.ItemData == null)
        {
            // Holding shift => Split stack.
            if (isSplittingStack && clickedUISlot.AssignedSlot.SplitStack(out InventorySlot halfSlot))
            {
                Debug.Log("Splitting stack");
                mouseInventoryItem.UpdateMouseSlot(halfSlot);
                clickedUISlot.UpdateUISlot();
                return;
            }
            else
            {
                mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedSlot);
                clickedUISlot.ClearUISlot();
                return;
            }
        }

        // Clicked slot doesn't have item & Mouse have item => place into the empty slot.
        if (clickedUISlot.AssignedSlot.ItemData == null && mouseInventoryItem.AssignedSlot.ItemData != null)
        {
            clickedUISlot.AssignedSlot.AssignItem(mouseInventoryItem.AssignedSlot);
            clickedUISlot.UpdateUISlot();
            mouseInventoryItem.ClearSlot();
            return;
        }


        // Both slots have an item
        // Both items the same => combine.
        // Slot stack size + mouse stack size > the slot Max Stack Size => take from mouse.
        // Different items => swap items.
        if (clickedUISlot.AssignedSlot.ItemData != null && mouseInventoryItem.AssignedSlot.ItemData != null)
        {
            bool isSameItem = clickedUISlot.AssignedSlot.ItemData == mouseInventoryItem.AssignedSlot.ItemData;

            if (isSameItem)
            {
                if (
                    clickedUISlot.AssignedSlot.ItemData == mouseInventoryItem.AssignedSlot.ItemData &&
                    clickedUISlot.AssignedSlot.EnoughRoomInStack(mouseInventoryItem.AssignedSlot.StackSize))
                {
                    clickedUISlot.AssignedSlot.AssignItem(mouseInventoryItem.AssignedSlot);
                    clickedUISlot.UpdateUISlot();

                    mouseInventoryItem.ClearSlot();
                }
                else if (
                    !clickedUISlot.AssignedSlot.EnoughRoomInStack(mouseInventoryItem.AssignedSlot.StackSize, out int leftInStack))
                {
                    if (leftInStack > 0)
                    {
                        int remainingOnMouse = mouseInventoryItem.AssignedSlot.StackSize - leftInStack;

                        clickedUISlot.AssignedSlot.AddToStack(leftInStack);
                        clickedUISlot.UpdateUISlot();

                        var newItem = new InventorySlot(mouseInventoryItem.AssignedSlot.ItemData, remainingOnMouse);
                        mouseInventoryItem.ClearSlot();
                        mouseInventoryItem.UpdateMouseSlot(newItem);
                    }
                    else
                    {
                        SwapSlots(clickedUISlot);
                    }
                }
            }
            else
            {
                SwapSlots(clickedUISlot);
            }
        }
    }

    private void SwapSlots(UIInventorySlot clickedUISlot)
    {
        var slotClone = new InventorySlot(mouseInventoryItem.AssignedSlot.ItemData, mouseInventoryItem.AssignedSlot.StackSize);
        mouseInventoryItem.ClearSlot();

        mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedSlot);

        clickedUISlot.ClearUISlot();

        clickedUISlot.AssignedSlot.AssignItem(slotClone);
        clickedUISlot.UpdateUISlot();
    }
}