using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
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
        spriteRenderer.sprite = cropData.GrowthSprites[growthStage];
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
            Destroy(gameObject);
        }
    }

    public void Interact()
    {
        if (growthStage == cropData.GrowthIntervals.Count - 1) {
            Harvest();
            return;
        }

        NextStage();
    }

    void Harvest() {
        ItemSpawner itemSpawner = ItemSpawner.Instance;
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
}