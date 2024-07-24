using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using Animancer;
using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;


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
    [SerializeField] private AudioClip hoeSound;
    [SerializeField] private AudioClip axeSound;
    [SerializeField] private AudioClip wateringCanSound;

    private AudioSource audioSource;

    private Dictionary<int, DirectionalAnimationSet> itemAnimationMapping = new();
    public Vector3Int prevHighlightedPos = new();
    private bool isRunning = false;
    private bool isInteracting = false;

    public PlayerData playerData;



    public Vector2 Direction
    {
        get => playerData.Direction;
        set => playerData.Direction = value;
    }
    [SerializeField] private AudioSource footstepAudioSource;
    [SerializeField] private AudioClip footstepClip;

    public Vector3 Position
    {
        get => playerData.position;
        set => playerData.position = value;
    }

    private DirectionalAnimationSet _CurrentAnimationSet;

    private PlayerControls playerControls;
    private Rigidbody2D playerRb;
    private Vector2 _Movement;
    private InventoryHolder inventoryHolder;
    private void Awake()
    {
        playerControls = new PlayerControls();
        playerRb = GetComponent<Rigidbody2D>();
        inventoryHolder = GetComponent<InventoryHolder>();

        itemAnimationMapping.Add(1, usingHoe);
        itemAnimationMapping.Add(13, usingAxe);
        itemAnimationMapping.Add(15, usingWateringCan);

        SaveGameManager.OnSaveGame += SavePlayer;
        SaveGameManager.OnLoadGame += LoadPlayer;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void SavePlayer()
    {
        playerData.position = Position;
        SaveGameManager.CurrentSaveData.playerData = playerData;
    }

    private void LoadPlayer(SaveData data)
    {
        playerData = data.playerData;
        Position = playerData.position;
        transform.position = Position;
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
        if (isInteracting)
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
    PlayerData GetPlayerData()
    {
        var playerData = GetComponent<PlayerController>().playerData;
        return playerData;
    }

    InventorySlot GetCurrentInventorySlot()
    {
        return inventoryHolder.GetItemInActiveSlot();
    }


    IEnumerator InteractingCountDown()
    {
        yield return new WaitForSeconds(0.5f);
        isInteracting = false;
    }

    RaycastHit2D CompareFunction(RaycastHit2D a, RaycastHit2D b)
    {
        var AInteractable = a.transform.gameObject.GetComponent<IPlayerInteractable>();
        var BInteractable = b.transform.gameObject.GetComponent<IPlayerInteractable>();
        if (AInteractable == null)
        {
            return b;
        }
        if (BInteractable == null)
        {
            return a;
        }
        if (AInteractable.Priority == BInteractable.Priority)
        {
            return a.distance < b.distance ? a : b;
        }
        return AInteractable.Priority > BInteractable.Priority ? a : b;
    }

    private void Interact()
{
    if (isInteracting)
    {
        return;
    }
    Vector2 direction = GetComponent<PlayerController>().playerData.Direction;
    RaycastHit2D[] hits = Physics2D.RaycastAll(playerRb.position, direction, 1.0f, LayerMask.GetMask("Interactable"));
    Debug.DrawRay(playerRb.position, direction, Color.red, 1.0f);
    Debug.Log("Hit " + hits.ToList().Count.ToString() + " objects");
    IPlayerInteractable.InteractionContext ctx = new(GetPlayerData(), GetCurrentInventorySlot());
    var hit = hits.Aggregate((a, b) => CompareFunction(a, b));
    Debug.Log("Hit " + hit.transform.gameObject.name);
    if (hit.collider.TryGetComponent<IPlayerInteractable>(out var item))
    {
        DirectionalAnimationSet chosenAnimation = null;
        var requiredItem = item.RequiredItem(ctx);
        if (requiredItem == null)
        {
            isInteracting = true;
            item.Interact(ctx);
            StartCoroutine(InteractingCountDown());
        }
        else
        {
            Debug.Log("Required item: " + requiredItem.ToString());
            if (itemAnimationMapping.ContainsKey(requiredItem.itemID))
            {
                chosenAnimation = itemAnimationMapping[requiredItem.itemID];
                isInteracting = true;
                item.Interact(ctx);
                if (chosenAnimation != null)
                {
                    var state = Play(chosenAnimation);
                    state.Events.OnEnd = () => isInteracting = false;

                    switch (requiredItem.itemID)
                    {
                        case 1:
                            audioSource.PlayOneShot(hoeSound);
                            break;
                        case 13:
                            audioSource.PlayOneShot(axeSound);
                            break;
                        case 15:
                            audioSource.PlayOneShot(wateringCanSound);
                            break;
                    }
                }
            }
        }
    }
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

    public void SetIdleAnim()
    {
        Play(idle);
    }
}
