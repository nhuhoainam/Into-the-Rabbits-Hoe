using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    Rigidbody2D rigi;
    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
    }

    void Interact()
    {
        RaycastHit2D hit = Physics2D.Raycast(rigi.position, Vector2.left, 1.0f, LayerMask.GetMask("Interactable"));
        if (hit.collider != null)
        {
            hit.collider.GetComponent<IPlayerInteractable>().Interact(GetPlayerData());
        }
    }

    PlayerData GetPlayerData()
    {
        var playerData = GetComponent<PlayerController>().playerData;
        return playerData;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Interact();
        }
    }
}
