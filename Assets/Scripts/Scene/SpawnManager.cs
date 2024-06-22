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
                if (GameObject.FindWithTag("Player").TryGetComponent<PlayerController>(out var player))
                {
                    player.playerData.position = spawnPoint.transform.position;
                    PlayerPrefs.DeleteKey("SpawnPoint");
                }
            }
        }
    }
}
