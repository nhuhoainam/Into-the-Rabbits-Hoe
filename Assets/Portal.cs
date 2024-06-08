using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private int sceneToLoad = -1;
    [SerializeField] private string spawnPointName; // Name of the spawn point to use
    private bool isTriggered = false; // Flag to prevent multiple triggers

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTriggered && other.gameObject.GetComponent<PlayerController>())
        {
            isTriggered = true; // Set the flag to true to prevent re-entry
            PlayerPrefs.SetString("SpawnPoint", spawnPointName);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
