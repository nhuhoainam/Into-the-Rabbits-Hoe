using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private void Awake()
    {
        // PlayerPrefs.DeleteKey("SpawnPoint"); 
        string spawnPointName = PlayerPrefs.GetString("SpawnPoint");
        if (!string.IsNullOrEmpty(spawnPointName))
        {
            GameObject spawnPoint = GameObject.Find(spawnPointName + "Position");
            if (spawnPoint != null)
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    player.transform.position = spawnPoint.transform.position;
                    PlayerPrefs.DeleteKey("SpawnPoint");
                }
            }
        }
    }
}