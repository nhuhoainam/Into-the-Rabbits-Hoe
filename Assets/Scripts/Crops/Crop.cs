using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour, IPlayerInteractable
{
    public CropData cropData;
    [SerializeField] int growthStage = 0;
    [SerializeField] float growthTime = 0;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update

    void Awake()
    {
        Debug.Log("Crop awake");
        SaveGameManager.OnSaveScene += SaveCrop;
        // DontDestroyOnLoad(gameObject);
    }

    void SaveCrop(int sceneIndex)
    {
        SaveGameManager.CurrentSaveData.sceneData[sceneIndex].cropSaveData.Add(new(growthStage, growthTime, gameObject.name));
    }

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
            var itemId = cropData.itemData.itemID;
            ItemSpawner.GetInstance().SpawnItem(itemId, transform.position);
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Debug.Log("Destroying crop");
        SaveGameManager.OnSaveScene -= SaveCrop;
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
        Debug.Log("Harvesting");
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
        Harvest();
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
