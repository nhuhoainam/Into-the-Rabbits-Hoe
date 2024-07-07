using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField] CropData cropData;
    [SerializeField] int growthStage = 0;
    [SerializeField] int growthTime = 0;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = cropData.GrowthSprites[growthStage];
        if (Input.GetKeyDown(KeyCode.Space))
        {
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
}
