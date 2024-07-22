using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopController : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    private ShopDisplay shopDisplay;

    private PlayerControls playerControls;

    void Awake()
    {
        playerControls = new PlayerControls();
        shopDisplay = shopPanel.GetComponentInChildren<ShopDisplay>();
        playerControls.Inventory.CloseInventory.performed += ctx => CloseShop();
    }

    void OnEnable()
    {
        playerControls.Inventory.Enable();
        DialogueManager.OnOpenShop += DisplayShop;
    }

    void OnDisable()
    {
        playerControls.Inventory.Disable();
        DialogueManager.OnOpenShop -= DisplayShop;
    }

    void DisplayShop(ShopKeeper shopKeeper)
    {
        shopPanel.SetActive(true);
        shopDisplay.ShopSystem = shopKeeper.shopSystem;
        shopDisplay.AssignSlots();
    }

    void CloseShop()
    {
        Debug.Log("Closing shop");
        shopPanel.SetActive(false);
        shopDisplay.ShopSystem = null;
        shopDisplay.ClearSlots();
    }
}