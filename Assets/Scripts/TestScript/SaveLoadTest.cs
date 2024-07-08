using System.Collections;
using System.Collections.Generic;
using SaveLoadSystem;
using UnityEngine;

public class SaveLoadTest : MonoBehaviour
{
    public void TestSave()
    {
        SaveGameManager.CurrentSaveData.playerName = "Phong";
        SaveGameManager.Save();
    }

    public void TestLoad()
    {
        SaveGameManager.Load();
        Debug.Log(SaveGameManager.CurrentSaveData.playerName);
    }
}
