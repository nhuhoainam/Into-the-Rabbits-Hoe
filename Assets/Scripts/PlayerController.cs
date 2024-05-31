using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;

    private PlayerControls playerControls;
    private Rigidbody2D playerRb;
    private Vector2 movement;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
    }

    private void MovePlayer()
    {
        playerRb.MovePosition(playerRb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
}
