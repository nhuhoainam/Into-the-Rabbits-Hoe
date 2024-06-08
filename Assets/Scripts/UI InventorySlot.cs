using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventorySlot : MonoBehaviour
{
    public ItemInstance itemInstance;

    public UIInventorySlot() {
        itemInstance = null;
    }

    public void SetItem(ItemInstance itemInstance)
    {
        this.itemInstance = itemInstance;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
