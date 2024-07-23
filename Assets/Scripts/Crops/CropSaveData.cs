using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CropSaveData : MonoBehaviour
{
    public int growthStage;
    public float growthTime;
    public string key;
    public CropSaveData(int growthStage, float growthTime, string key)
    {
        this.growthStage = growthStage;
        this.growthTime = growthTime;
        this.key = key;
    }
}
