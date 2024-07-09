using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using Animancer;
using System;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float runSpeedModifier = 2.0f;
    [SerializeField] private DirectionalAnimationSet idle;
    [SerializeField] private DirectionalAnimationSet walking;
    [SerializeField] private DirectionalAnimationSet running;
    [SerializeField] private DirectionalAnimationSet usingHoe;
    [SerializeField] private DirectionalAnimationSet usingAxe;
    [SerializeField] private DirectionalAnimationSet usingWateringCan;

    [SerializeField] private AnimancerComponent _Animancer;
    public Vector3Int prevHighlightedPos = new();
    private bool isRunning = false;
    private bool isUsingTool = false;

    public PlayerData playerData;

    public Vector2 Direction {
        get => playerData.Direction;
        set => playerData.Direction = value;
    }
    [SerializeField] private AudioSource footstepAudioSource; 
    [SerializeField] private AudioClip footstepClip; 

    public Vector3 Position {
        get => playerData.position;
        set => playerData.position = value;
    }

    private DirectionalAnimationSet _CurrentAnimationSet;

    private PlayerControls playerControls;
    private Rigidbody2D playerRb;
    private Vector2 _Movement;
    private SpriteRenderer spriteRenderer;
    private int defaultOrderInLayer = 100;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        SaveGameManager.OnSaveGame += SavePlayer;
        SaveGameManager.OnLoadGame += LoadPlayer;
    }

    private void SavePlayer()
    {
        SaveGameManager.CurrentSaveData.playerData = playerData;
    }

    private void LoadPlayer(SaveData data)
    {
        playerData = data.playerData;
        transform.position = playerData.position;
        Direction = playerData.Direction;
    }

    private void Start()
    {
        playerControls.Movement.Run.performed += ctx => isRunning = true;
        playerControls.Movement.Run.canceled += ctx => isRunning = false;
        playerControls.Interaction.Interact.performed += ctx => Interact();
    }
    
    private AnimancerState Play(DirectionalAnimationSet animations)
    {
        _CurrentAnimationSet = animations;
        var state = _Animancer.Play(animations.GetClip(Direction));

        return state;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Slopes"))
        {
            // Update player's sorting order to match the slope's sorting order
            Tilemap slopeTilemap = other.GetComponent<Tilemap>();
            if (slopeTilemap != null)
            {
                spriteRenderer.sortingOrder = slopeTilemap.GetComponent<TilemapRenderer>().sortingOrder + 1;
            }
            else // If no slope is found, revert to the default sorting order
            {
                spriteRenderer.sortingOrder = defaultOrderInLayer;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Slopes"))
        {
            // Find the ground tilemap the player is currently on
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Grass"))
                {
                    Tilemap groundTilemap = collider.GetComponent<Tilemap>();
                    if (groundTilemap != null)
                    {
                        // Update player's sorting order to match the ground's sorting order
                        spriteRenderer.sortingOrder = groundTilemap.GetComponent<TilemapRenderer>().sortingOrder + 4;
                        return;
                    }
                }
            }

            // If no ground is found, revert to the default sorting order
            spriteRenderer.sortingOrder = defaultOrderInLayer;
        }
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
        if (isUsingTool)
        {
            return;
        }
        _Movement = playerControls.Movement.Move.ReadValue<Vector2>();

        if (_Movement != Vector2.zero)
        {
            Direction = _Movement;

            UpdateMovementState();
            Direction = _CurrentAnimationSet.Snap(Direction);
            _Movement = Vector2.ClampMagnitude(_Movement, 1);

            if (!footstepAudioSource.isPlaying)
            {
                footstepAudioSource.clip = footstepClip;
                footstepAudioSource.Play();
            }
        }
        else
        {
            Play(idle);

            if (footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Stop();
            }
        }
    }


    private void UpdateMovementState() 
    {
        Play(isRunning ? running : walking);
    }

    private void MovePlayer()
    {
        playerRb.MovePosition(playerRb.position 
            + _Movement 
            * (moveSpeed 
                * (isRunning ? runSpeedModifier : 1.0f) 
                * Time.fixedDeltaTime));
    }

    private void Interact()
    {
        if (isUsingTool)
        {
            return;
        }
        isUsingTool = true;
        var state = Play(usingHoe);
        state.Events.OnEnd = () => isUsingTool = false;
    }

    private void Update()
    {
        Position = transform.position;
        PlayerInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void DisableInput()
    {
        playerControls.Disable();
    }

    public void EnableInput()
    {
        playerControls.Enable();
    }

    public void SetIdleAnim() {
        Play(idle);
    }
}
