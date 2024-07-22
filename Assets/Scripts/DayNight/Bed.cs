using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour, IPlayerInteractable
{
    private DayNightSystem2D _dayNightSystem;

    private bool _isSleeping = false;

    void Start()
    {
        _dayNightSystem = FindObjectOfType<DayNightSystem2D>();
    }

    public void Interact(PlayerData playerData)
    {
        Debug.Log("Interacting with bed");
        if (_isSleeping)
        {
            return;
        }
        _isSleeping = true;
        Time.timeScale = 10f;
    }

    void Update()
    {
        if (_isSleeping && _dayNightSystem.dayCycle == DayCycles.Sunrise)
        {
            _isSleeping = false;
            Time.timeScale = 1f;
        }
    }
}
