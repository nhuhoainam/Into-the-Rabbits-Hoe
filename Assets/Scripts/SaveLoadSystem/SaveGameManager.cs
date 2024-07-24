using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static class SaveGameManager
{
    public static SaveData CurrentSaveData = new();
    public static UnityAction OnSaveGame;
    public static UnityAction<SaveData> OnLoadGame;
    public static UnityAction<int> OnSaveScene;
    public static UnityAction<SaveData, int> OnLoadScene;

    public const string SaveDirectory = "/SaveData/";
    public const string Filename = "SaveGame.json";

    public static bool Save()
    {
        CurrentSaveData.currentScene = SceneManager.GetActiveScene().buildIndex;
        SaveScene(CurrentSaveData.currentScene);
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
            SceneManager.LoadScene(tempData.currentScene);
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

    public static void SaveScene(int sceneIndex)
    {
        if (!CurrentSaveData.sceneData.ContainsKey(sceneIndex))
            CurrentSaveData.sceneData.Add(sceneIndex, new SceneData());
        OnSaveScene?.Invoke(sceneIndex);
    }

    public static void LoadScene(SaveData data, int sceneIndex)
    {
        Debug.Log("Loading scene: " + sceneIndex);
        if (!data.sceneData.ContainsKey(sceneIndex)) {
            data.sceneData.Add(sceneIndex, new SceneData());
            return;
        }
        OnLoadScene?.Invoke(data, sceneIndex);
        data.sceneData.Remove(sceneIndex);
        data.sceneData.Add(sceneIndex, new SceneData());
    }
}
