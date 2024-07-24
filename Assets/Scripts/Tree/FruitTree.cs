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
    private Animator fruitDropAnimator;
    [SerializeField] private float timeUntilFruit = 240;
    [SerializeField] private bool hasFruit = true;

    enum State
    {
        Chopped,
        HasFruit,
        NoFruit,
    }
    void Awake()
    {
        SaveGameManager.OnSaveScene += SaveTree;
        // DontDestroyOnLoad(gameObject);
    }

    private void SaveTree(int sceneIndex)
    {
        SaveGameManager.CurrentSaveData.sceneData[sceneIndex].treeSaveData.Add(new (hasFruit, timeUntilFruit, gameObject.name));
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        fruitDropAnimator = transform.GetChild(0).GetComponent<Animator>();
        if (hasFruit)
        {
            animator.SetTrigger("HaveFruit");
        }
    }

    void OnDestroy()
    {
        SaveGameManager.OnSaveScene -= SaveTree;
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

    IEnumerator SpawnDroppedWood(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        int woodAmount = UnityEngine.Random.Range(1, 3);
        Debug.Log(woodAmount);
        for (int i = 0; i < woodAmount; i++)
        {
            float randomYOffset = UnityEngine.Random.Range(-1, 1);
            float randomXOffset = UnityEngine.Random.Range(-1, 1);
            var position = transform.position + new Vector3(randomXOffset, randomYOffset, 0);
            ItemSpawner.GetInstance().SpawnItem(2, position);
        }
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
            if (hasFruit)
            {
                fruitDropAnimator.SetTrigger("Drop");
                var duration = animator.GetCurrentAnimatorClipInfo(0).Length;
                StartCoroutine(SpawnDroppedFruits(duration));
                StartCoroutine(SpawnDroppedWood(duration));
            }
            // Change layer to disable interaction
            gameObject.layer = LayerMask.NameToLayer("Default");
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

    int IPlayerInteractable.Priority => 2;
}
