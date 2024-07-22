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

    void Update()
    {
        if (_isSleeping && _dayNightSystem.dayCycle == DayCycles.Sunrise)
        {
            _isSleeping = false;
            Time.timeScale = 1f;
        }
    }

    public void Interact(IPlayerInteractable.InteractionContext ctx)
    {
        Debug.Log("Interacting with bed");
        if (_isSleeping)
        {
            return;
        }
        _isSleeping = true;
        Time.timeScale = 100f;
    }

    public ItemData RequiredItem(IPlayerInteractable.InteractionContext ctx)
    {
        return null;
    }
}
