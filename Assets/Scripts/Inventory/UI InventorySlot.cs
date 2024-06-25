using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIInventorySlot : MonoBehaviour, IDropHandler
{
    public GameObject itemDisplayPrefab;
    private UIItemDisplay itemDisplay;

    public int slotIndex;
    public bool isActiveSlot;

    public delegate void OnMouseDrop(GameObject dropped, int slotIndex, bool isActiveSlot);
    public OnMouseDrop onMouseDrop;

    void Awake()
    {
        itemDisplay = null;
    }

    public void SetItem(ItemInstance newItemInstance)
    {
        try
        {
            var itemDisplayTransform = transform.GetChild(0);
            itemDisplay = itemDisplayTransform.gameObject.GetComponent<UIItemDisplay>();
        }
        catch (System.Exception)
        {
            itemDisplay = Instantiate(itemDisplayPrefab, transform).GetComponent<UIItemDisplay>();
        }
        itemDisplay.SetItem(newItemInstance);
    }

    public void ClearItem()
    {
        if (itemDisplay != null)
        {
            Destroy(itemDisplay.gameObject);
        }
        itemDisplay = null;
    }

    public void OnDrop(PointerEventData pointer)
    {
        GameObject dropped = pointer.pointerDrag;
        onMouseDrop.Invoke(dropped, slotIndex, isActiveSlot);
    }
}
