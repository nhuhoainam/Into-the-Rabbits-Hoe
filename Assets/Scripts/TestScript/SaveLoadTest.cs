using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadTest : MonoBehaviour
{
    public void TestSave()
    {
        SaveGameManager.Save();
    }

    public void TestLoad()
    {
        SaveGameManager.Load();
    }

    public void TestDelete()
    {
        SaveGameManager.Delete();
    }
}
