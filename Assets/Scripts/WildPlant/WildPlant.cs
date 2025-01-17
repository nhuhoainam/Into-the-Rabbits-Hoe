using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildPlant : MonoBehaviour, IPlayerInteractable
{
    public CropData cropData;
    [SerializeField] int growthStage = 0;
    [SerializeField] float growthTime = 0;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // growthTime += Time.deltaTime;
        var sprite = cropData.GrowthSprites[growthStage];
        if (sprite != null)
        {
            spriteRenderer.sprite = sprite;
        }
        if (growthStage < cropData.GrowthIntervals.Count - 1 && growthTime >= cropData.GrowthIntervals[growthStage])
        {
            growthTime = 0;
            NextStage();
        }
    }

    void NextStage()
    {
        growthStage++;
        if (growthStage >= cropData.GrowthSprites.Count)
        {
            ItemSpawner.GetInstance().SpawnItem(300, transform.position);
            Destroy(gameObject);
        }
    }

    public void Interact()
    {
        if (growthStage == cropData.GrowthIntervals.Count - 1) {
            Harvest();
            return;
        }
    }

    void Harvest() {
        ItemSpawner itemSpawner = ItemSpawner.GetInstance();
        itemSpawner.SpawnItem(300, transform.position);
        if (cropData.Regrowable)
        {
            growthStage = 0;
            growthTime = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Interact(IPlayerInteractable.InteractionContext ctx)
    {
        return;
    }

    ItemData IPlayerInteractable.RequiredItem(IPlayerInteractable.InteractionContext ctx)
    {
        return null;
    }

    int IPlayerInteractable.Priority
    {
        get
        {
            return 1;
        }
    }
}
