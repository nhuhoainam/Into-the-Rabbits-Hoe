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

    private Vector2 Direction {
        get => playerData.curDirection;
        set => playerData.curDirection = value;
    }
    private DirectionalAnimationSet _CurrentAnimationSet;

    private PlayerControls playerControls;
    private Rigidbody2D playerRb;
    private Vector2 _Movement;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerRb = GetComponent<Rigidbody2D>();
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
        }
        else
        {
            Play(playerData.idle);
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
}
