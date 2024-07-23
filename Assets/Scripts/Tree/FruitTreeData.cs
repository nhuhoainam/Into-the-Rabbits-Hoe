using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FruitTreeData
{
    public bool hasFruit;
    public float timeUntilFruit;
    public string key;
    public FruitTreeData(bool hasFruit, float timeUntilFruit, string key)
    {
        this.hasFruit = hasFruit;
        this.timeUntilFruit = timeUntilFruit;
        this.key = key;
    }
}
