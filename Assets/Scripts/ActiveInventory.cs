using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private int _activeSlot = 0;

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        playerControls.Inventory.Active.performed += ctx => ChangeActiveSlot((int)ctx.ReadValue<float>());
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void ChangeActiveSlot(int slot)
    {
        ChangeActiveHighlight(slot - 1);
    }

    private void ChangeActiveHighlight(int index)
    {
        _activeSlot = index;

        foreach (Transform inventorySlot in transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        transform.GetChild(_activeSlot).GetChild(0).gameObject.SetActive(true);
    }
}
