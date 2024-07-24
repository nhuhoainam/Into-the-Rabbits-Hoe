using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISellSlot : MonoBehaviour
{
    [SerializeField] private ShopDisplay ParentDisplay;
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnSellClick);
    }

    public void OnSellClick()
    {
        Debug.Log("Sell clicked");
        ParentDisplay.SellClicked();
    }
}
