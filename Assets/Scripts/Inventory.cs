using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryData inventoryData;

    public GameObject uiInventoryRef;
    public GameObject uiActiveInventoryRef;

    void Awake()
    {
        // Create a new inventory
        inventoryData = InventoryData.CreateInstance<InventoryData>();
        uiInventoryRef.GetComponent<UIInventory>().inventoryData = inventoryData;
        uiActiveInventoryRef.GetComponent<UIActiveInventory>().inventoryData = inventoryData;
    }

    // Start is called before the first frame update
    void Start()
    {
        var hoe = AssetDatabase.LoadAssetAtPath<ItemData>("Assets/Scripts/Item_Hoe.asset");
        var ok = inventoryData.AddActiveItem(new ItemInstance(hoe));
        inventoryData.AddItem(new ItemInstance(hoe));
        Debug.Log("Adding hoe to active inventory: " + ok);
        
        // Initialize the inventory 
        uiActiveInventoryRef.GetComponent<UIActiveInventory>().Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
