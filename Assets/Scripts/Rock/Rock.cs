using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour, IPlayerInteractable
{
    [SerializeField] private int health = 3;
    // Start is called before the first frame update
    void IPlayerInteractable.Interact(PlayerData playerData)
    {
        if (health > 0)
        {
            health--;
            if (health == 0)
            {
                Destroy(gameObject);
                ItemSpawner.Instance.SpawnItem(6, transform.position);
            }
        }
    }
}
