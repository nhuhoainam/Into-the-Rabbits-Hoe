using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image image;
    [HideInInspector] public Transform parentAfterDrag;

    public ItemInstance itemInstance;
    public int slotIndex;
    public bool isActiveSlot;

    void Awake()
    {
        image = transform.GetChild(0).GetComponent<Image>();
    }

    void Start()
    {
        itemInstance = transform.GetComponent<UIItemDisplay>().itemInstance;
        slotIndex = transform.parent.GetComponent<UIInventorySlot>().slotIndex;
        isActiveSlot = transform.parent.GetComponent<UIInventorySlot>().isActiveSlot;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        transform.localPosition = Vector3.zero;
        image.raycastTarget = true;
    }
}
