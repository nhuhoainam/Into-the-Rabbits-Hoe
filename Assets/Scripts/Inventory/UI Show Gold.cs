using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIShowGold : MonoBehaviour
{
    public PlayerController player;
    [SerializeField] private TextMeshProUGUI goldText;

    void Awake()
    {
        PlayerController.OnGoldChanged += UpdateGold;
    }

    void Start()
    {
        UpdateGold();
    }

    void OnDestroy()
    {
        PlayerController.OnGoldChanged -= UpdateGold;
    }

    void UpdateGold()
    {
        goldText.text = player.Money.ToString();
    }
}
