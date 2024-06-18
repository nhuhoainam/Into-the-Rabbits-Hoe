using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private int sceneToLoad = -1;
    [SerializeField] private string spawnPointName; // Name of the spawn point to use
    private bool isTriggered = false; // Flag to prevent multiple triggers

    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (!isTriggered && other.gameObject.GetComponent<PlayerController>())
        {
            isTriggered = true; // Set the flag to true to prevent re-entry
            var transAnim = GameObject.FindWithTag("TransitionAnimation");
            if (transAnim == null)
            {
                Debug.LogWarning("No transition animation found in the scene");
                yield break;
            }
            PlayerPrefs.SetString("SpawnPoint", spawnPointName);
            // Set the state of the player to idle corresponding to the direction
            other.gameObject.GetComponent<PlayerController>().SetIdleState();
            // Ignore the input until next scene is loaded
            other.gameObject.GetComponent<PlayerController>().enabled = false;
            transAnim.GetComponent<Animator>().SetTrigger("End");
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene(sceneToLoad);
            transAnim.GetComponent<Animator>().SetTrigger("Start");
            other.gameObject.GetComponent<PlayerController>().enabled = true;
        }
    }
}