using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    private GameObject persistentObject;
    void Awake() 
    {
        persistentObject = Instantiate(prefab);
        persistentObject.SetActive(false);
    }

    public void Instructions()
    {
        //Show instructions
    }

    public void NewGame()
    {
        SaveGameManager.Delete();
        SaveGameManager.CurrentSaveData = new();
        persistentObject.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + SaveGameManager.SaveDirectory + SaveGameManager.Filename)) {
            persistentObject.SetActive(true);
            SaveGameManager.Load();
        }
    }

    public void Settings()
    {
        //Settings
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
