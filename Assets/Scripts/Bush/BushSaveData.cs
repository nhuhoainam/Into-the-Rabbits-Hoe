using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BushSaveData
{
    public bool hasBerries;
    public float lastCheckBerrySpawnTime;
    public string key;
    public Vector3 position;
    public BushSaveData(Vector3 position, bool hasBerries, float lastCheckBerrySpawnTime, string key)
    {
        this.position = position;
        this.hasBerries = hasBerries;
        this.lastCheckBerrySpawnTime = lastCheckBerrySpawnTime;
        this.key = key;
    }
}
