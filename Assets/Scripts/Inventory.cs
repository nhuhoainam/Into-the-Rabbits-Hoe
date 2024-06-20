using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public PlayerData playerData;
    public InventoryData inventoryData;

    public GameObject uiInventoryRef;
    public GameObject uiActiveInventoryRef;
    private UIInventory uiInventory;
    private UIActiveInventory uiActiveInventory;

    public GameObject droppedItemPrefab;

    public PlayerControls playerControls;

    void Awake()
    {
        playerControls = new PlayerControls();

        // Create a new inventory
        inventoryData = InventoryData.CreateInstance<InventoryData>();
        uiInventory = uiInventoryRef.GetComponent<UIInventory>();
        uiActiveInventory = uiActiveInventoryRef.GetComponent<UIActiveInventory>();
        uiInventory.inventoryData = inventoryData;
        uiActiveInventory.inventoryData = inventoryData;
    }

    void OnEnable()
    {
        playerControls.Enable();
    }

    void OnDisable()
    {
        playerControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerControls.Inventory.Active.performed += ctx => uiActiveInventory.ChangeActiveSlot((int)ctx.ReadValue<float>());
        playerControls.Inventory.DropItem.performed += ctx => DropItem(inventoryData.currentSlot, true);
        playerControls.Inventory.OpenInventory.performed += ctx => uiInventory.ToggleInventory();

        var hoe = AssetDatabase.LoadAssetAtPath<ItemData>("Assets/Scripts/Item_Hoe.asset");
        var wood = AssetDatabase.LoadAssetAtPath<ItemData>("Assets/Scripts/Item_Wood.asset");
        inventoryData.AddActiveItem(new ItemInstance(hoe));
        inventoryData.AddItem(new ItemInstance(hoe));
        inventoryData.AddItem(new ItemInstance(wood, quantity: 6), out _);

        uiActiveInventory.UpdateInventory();
    }

    public void AddItem(ItemInstance item) {
        inventoryData.AddItem(item, out bool addedToActive);
        if (addedToActive) {
            uiActiveInventory.UpdateInventory();
        } else {
            uiInventory.UpdateInventory();
        }
    }

    public void DropItem(int itemIndex, bool active = false) {
        ItemInstance item = active ? inventoryData.activeItems[itemIndex] : inventoryData.items[itemIndex];
        if (item != null) {
            if (active) {
                inventoryData.RemoveActiveItem(itemIndex);
            } else {
                inventoryData.RemoveItem(itemIndex);
            }
            GameObject droppedItem = Instantiate(droppedItemPrefab);
            droppedItem.GetComponent<ItemInstanceContainer>().item = item;
            droppedItem.GetComponent<SpriteRenderer>().sprite = item.itemData.itemSprite;
            droppedItem.transform.position = transform.position + new Vector3(playerData.curDirection.x, playerData.curDirection.y, 0) * 2;
        }
        if (active) {
            uiActiveInventory.UpdateInventory();
        } else {
            uiInventory.UpdateInventory();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ItemInstanceContainer foundItem))
        {
            AddItem(foundItem.TakeItem());
        }
    }
}
