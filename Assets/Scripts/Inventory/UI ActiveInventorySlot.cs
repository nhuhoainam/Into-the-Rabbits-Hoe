using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActiveInventorySlot : MonoBehaviour
{
    public UIInventorySlot slot;

    public GameObject ActiveSlotIndicator { get; private set; }

    void Awake()
    {
        slot = GetComponentInChildren<UIInventorySlot>();
        ActiveSlotIndicator = transform.GetChild(1).gameObject;
    }
}