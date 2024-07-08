using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[ExecuteInEditMode]
public class UniqueID : MonoBehaviour
{
    [SerializeField] private string id = Guid.NewGuid().ToString();
    [SerializeField] private static SerializableDictionary<string, GameObject> idDatabase = new();

    public string ID => id;

    private void OnValidate()
    {
        if (idDatabase.ContainsKey(id))
            Generate();
        else
            idDatabase.Add(id, gameObject);
    }

    void OnDestroy()
    {
        if (idDatabase.ContainsKey(id))
            idDatabase.Remove(id);
    }

    private void Generate()
    {
        id = Guid.NewGuid().ToString();
        idDatabase.Add(id, gameObject);
    }
}
