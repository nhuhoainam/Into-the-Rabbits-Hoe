using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePanelController : MonoBehaviour
{
    public GameObject persistentObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveGameButton() {
        SaveGameManager.Save();
    }

    public void SaveAndExitButton() {
        SaveGameManager.Save();
        // Return to main menu (scene 0 in build settings)
        Destroy(persistentObject);
        SceneManager.LoadScene(0);
    }
}
