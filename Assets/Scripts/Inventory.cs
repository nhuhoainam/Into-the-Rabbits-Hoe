using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public PlayerData playerData;
    public InventoryData inventoryData;

    public GameObject uiInventoryRef;
    private UIInventory uiInventory;

    public GameObject droppedItemPrefab;

    public PlayerControls playerControls;

    void Awake()
    {
        playerControls = new PlayerControls();
        
        // Create a new inventory
        inventoryData = new();
        uiInventory = uiInventoryRef.GetComponent<UIInventory>();
        uiInventory.inventoryData = inventoryData;
        uiInventory.Init();
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
        playerControls.Inventory.DropItem.performed += ctx => DropItem(inventoryData.currentSlot, true);

        var hoe = AssetDatabase.LoadAssetAtPath<ItemData>("Assets/Scripts/Item_Hoe.asset");
        var wood = AssetDatabase.LoadAssetAtPath<ItemData>("Assets/Scripts/Item_Wood.asset");
        inventoryData.AddActiveItem(new ItemInstance(hoe));
        inventoryData.AddItem(new ItemInstance(hoe));
        inventoryData.AddItem(new ItemInstance(wood, quantity: 6), out _);

        uiInventory.UpdateInventory();
    }

    public void AddItem(ItemInstance item) {
        inventoryData.AddItem(item, out bool addedToActive);
        uiInventory.UpdateInventory(addedToActive);
    }

    public void DropItem(int itemIndex, bool active = false) {
        ItemInstance item = active ? inventoryData.activeItems[itemIndex] : inventoryData.items[itemIndex];
        if (item != null) {
            GameObject droppedItem = Instantiate(droppedItemPrefab);
            droppedItem.GetComponent<ItemInstanceContainer>().item = item;
            droppedItem.GetComponent<SpriteRenderer>().sprite = item.itemData.itemSprite;
            droppedItem.transform.position = transform.position + new Vector3(playerData.curDirection.x, playerData.curDirection.y, 0) * 2;
            if (active) {
                inventoryData.RemoveActiveItem(itemIndex);
            } else {
                inventoryData.RemoveItem(itemIndex);
            }
        }
        uiInventory.UpdateInventory(active);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ItemInstanceContainer foundItem))
        {
            AddItem(foundItem.TakeItem());
        }
    }
}
