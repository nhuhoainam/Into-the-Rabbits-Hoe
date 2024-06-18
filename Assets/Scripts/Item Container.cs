using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstanceContainer : MonoBehaviour
{
    public ItemInstance item;

    public ItemInstance TakeItem()
    {   
        Destroy(gameObject);
        return item;
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
