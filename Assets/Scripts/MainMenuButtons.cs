using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject persistentObject;
    void Awake() {
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
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        persistentObject.SetActive(true);
        SaveGameManager.Load();
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
