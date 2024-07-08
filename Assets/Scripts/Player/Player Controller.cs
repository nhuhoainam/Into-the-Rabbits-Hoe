using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using Animancer;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AnimancerComponent _Animancer;

    public PlayerData playerData;
    [SerializeField] private AudioSource footstepAudioSource; 
    [SerializeField] private AudioClip footstepClip; 
    private Vector2 Direction {
        get => playerData.curDirection;
        set => playerData.curDirection = value;
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
    }

    private void Start()
    {
        playerControls.Movement.Run.performed += ctx => playerData.isRunning = true;
        playerControls.Movement.Run.canceled += ctx => playerData.isRunning = false;
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
        if (playerData.isUsingTool)
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
            Play(playerData.idle);

            if (footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Stop();
            }
        }
    }


    private void UpdateMovementState() 
    {
        Play(playerData.isRunning ? playerData.running : playerData.walking);
    }

    private void MovePlayer()
    {
        playerRb.MovePosition(playerRb.position 
            + _Movement 
            * (playerData.moveSpeed 
                * (playerData.isRunning ? playerData.runSpeedModifier : 1.0f) 
                * Time.fixedDeltaTime));
    }

    private void Interact()
    {
        if (playerData.isUsingTool)
        {
            return;
        }
        playerData.isUsingTool = true;
        var state = Play(playerData.usingHoe);
        state.Events.OnEnd = () => playerData.isUsingTool = false;
    }

    private void Update()
    {
        playerData.position = transform.position;
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
        Play(playerData.idle);
    }
}
