using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CropSaveData : MonoBehaviour
{
    public Vector3 position;
    public int growthStage;
    public float growthTime;
    public string key;
    public CropSaveData(Vector3 position, int growthStage, float growthTime, string key)
    {
        this.position = position;
        this.growthStage = growthStage;
        this.growthTime = growthTime;
        this.key = key;
    }
}
