using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using System;

public class FruitTree : MonoBehaviour, IPlayerInteractable
{
    static readonly int MaxHeatlh = 100;
    [SerializeField] private int health = MaxHeatlh;

    private Animator animator;
    [SerializeField] private float timeUntilFruit = 240;
    [SerializeField] private bool hasFruit = true;

    enum State
    {
        Chopped,
        HasFruit,
        NoFruit,
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        if (hasFruit)
        {
            animator.SetTrigger("HaveFruit");
        }
    }

    void HarvestFruit()
    {
        if (!hasFruit)
        {
            return;
        }
        animator.SetTrigger("Drop");
        hasFruit = false;
        float len = animator.GetCurrentAnimatorClipInfo(0).Length;
        StartCoroutine(SpawnDroppedFruits(len));
    }


    IEnumerator SpawnDroppedFruits(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        foreach (var position in GetFruitPositions())
        {
            ItemSpawner.GetInstance().SpawnItem(0, position);
        }
    }

    List<Vector3> GetFruitPositions()
    {
        var root = transform.position + new Vector3(0, 0.2f, 0);
        List<Vector3> positions = new()
        {
            root + new Vector3(1.5f, 0.4f, 0),
            root + new Vector3(-1.5f, 0, 0),
            root + new Vector3(1.2f, -0.2f, 0)
        };
        return positions;
    }

    void IPlayerInteractable.Interact(IPlayerInteractable.InteractionContext ctx)
    {
        if (ctx.InventorySlot.ItemData == null || ctx.InventorySlot.ItemData.itemName != "Axe")
        {
            HarvestFruit();
        }
        else
        {
            ChopTree();
        }
    }

    void ChopTree()
    {
        health -= 20;
        if (health <= 0)
        {
            animator.SetTrigger("Fall");
            return;
        }
        animator.SetTrigger("Shake");
    }

    void GrowFruit()
    {
        if (hasFruit)
        {
            return;
        }

        hasFruit = true;
        animator.SetTrigger("Grow");
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasFruit)
        {
            timeUntilFruit -= Time.deltaTime;
            if (timeUntilFruit <= 0)
            {
                GrowFruit();
                timeUntilFruit = 240;
            }
        }
    }

    ItemData IPlayerInteractable.RequiredItem(IPlayerInteractable.InteractionContext ctx)
    {
        if (ctx.InventorySlot.ItemData == null)
        {
            return null;
        }
        if (ctx.InventorySlot.ItemData.itemName == "Axe")
        {
            return ctx.InventorySlot.ItemData;
        }
        else
        {
            return null;
        }
    }

    int IPlayerInteractable.Priority
    {
        get
        {
            return 1;
        }
    }
}