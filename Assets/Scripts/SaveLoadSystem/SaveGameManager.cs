using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public static class SaveGameManager
{
    public static SaveData CurrentSaveData = new();

    public static UnityAction OnSaveGame;
    public static UnityAction<SaveData> OnLoadGame;

    public const string SaveDirectory = "/SaveData/";
    public const string Filename = "SaveGame.json";

    public static bool Save()
    {
        OnSaveGame?.Invoke();

        var dir = Application.persistentDataPath + SaveDirectory;

        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        string json = JsonUtility.ToJson(CurrentSaveData, prettyPrint: true);
        File.WriteAllText(dir + Filename, json);

        GUIUtility.systemCopyBuffer = dir + Filename;

        return false;
    }

    public static void Load()
    {
        string fullPath = Application.persistentDataPath + SaveDirectory + Filename;
        SaveData tempData;

        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            tempData = JsonUtility.FromJson<SaveData>(json);

            OnLoadGame?.Invoke(tempData);
        }
        else
        {
            Debug.LogError("Save file not found!");
            tempData = new SaveData();
        }
        CurrentSaveData = tempData;
    }

    public static void Delete()
    {
        string fullPath = Application.persistentDataPath + SaveDirectory + Filename;

        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }
}
