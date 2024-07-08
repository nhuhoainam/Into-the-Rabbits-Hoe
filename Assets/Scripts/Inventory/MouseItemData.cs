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

    public float dropOffset = 2f;

    private ItemSpawner itemSpawner;

    private Transform playerTransform;

    void Awake()
    {
        itemSprite.preserveAspect = true;
        itemSprite.color = Color.clear;
        amount.text = "";

        itemSpawner = GetComponent<ItemSpawner>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerTransform == null) Debug.LogError("Player not found.");
    }

    public void UpdateMouseSlot(InventorySlot slot)
    {
        AssignedSlot.AssignItem(slot);
        UpdateMouseSlot();
    }

    public void UpdateMouseSlot()
    {
        itemSprite.sprite = AssignedSlot.ItemData.itemSprite;
        itemSprite.color = Color.white;
        if (AssignedSlot.StackSize > 1) amount.text = AssignedSlot.StackSize.ToString();
        else amount.text = "";
    }

    void Update()
    {
        if (AssignedSlot.ItemData != null)
        {
            transform.position = Mouse.current.position.ReadValue();
            // Change to use PlayerControls
            if (Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject())
            {
                Debug.Log("Dropped item");

                var playerDir = playerTransform.gameObject.GetComponent<PlayerController>().Direction;
                var dropPosition = playerTransform.position + new Vector3(playerDir.x * dropOffset, playerDir.y * dropOffset, 0f);
                itemSpawner.SpawnItem(AssignedSlot.ItemData.itemID, dropPosition, AssignedSlot.StackSize);

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

        return results.Exists(x => x.gameObject.layer == LayerMask.NameToLayer("UI"));
    }

}
