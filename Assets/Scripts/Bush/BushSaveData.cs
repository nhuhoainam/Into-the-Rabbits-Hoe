using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BushSaveData
{
    public bool hasBerries;
    public float lastCheckBerrySpawnTime;
    public string key;
    public BushSaveData(bool hasBerries, float lastCheckBerrySpawnTime, string key)
    {
        this.hasBerries = hasBerries;
        this.lastCheckBerrySpawnTime = lastCheckBerrySpawnTime;
        this.key = key;
    }
}
