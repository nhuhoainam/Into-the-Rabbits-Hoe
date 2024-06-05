using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Animancer;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float runSpeedModifier = 2.0f;

    [SerializeField] private AnimancerComponent _Animancer;
    [SerializeField] private DirectionalAnimationSet _Idle;
    [SerializeField] private DirectionalAnimationSet _Walking;
    [SerializeField] private DirectionalAnimationSet _Running;
    [SerializeField] private DirectionalAnimationSet _UsingHoe;
    [SerializeField] private DirectionalAnimationSet _UsingAxe;
    [SerializeField] private DirectionalAnimationSet _UsingWateringCan;
    [SerializeField] private Vector2 _Direction = Vector2.down;
    
    private DirectionalAnimationSet _CurrentAnimationSet;

    // private TimeSynchronizationGroup _MovementSynchronization;

    private PlayerControls playerControls;
    private Rigidbody2D playerRb;
    private Vector2 _Movement;

    private bool isRunning = false;

    private bool isUsingTool = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerRb = GetComponent<Rigidbody2D>();
        // _MovementSynchronization = new TimeSynchronizationGroup(_Animancer) { _Idle, _Walking };
    }

    private void Start()
    {
        playerControls.Movement.Run.performed += ctx => isRunning = true;
        playerControls.Movement.Run.canceled += ctx => isRunning = false;
        playerControls.Interaction.Interact.performed += ctx => Interact();
    }
    
    private AnimancerState Play(DirectionalAnimationSet animations)
    {
        // _MovementSynchronization.StoreTime(_CurrentAnimationSet);

        _CurrentAnimationSet = animations;
        var state = _Animancer.Play(animations.GetClip(_Direction));

        // _MovementSynchronization.SyncTime(_CurrentAnimationSet);
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

    private void FindTile()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _Direction, 1.0f);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
        }
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
            _Direction = _Movement;

            UpdateMovementState();
            _Movement = _CurrentAnimationSet.Snap(_Movement);
            _Movement = Vector2.ClampMagnitude(_Movement, 1);
        }
        else
        {
            Play(_Idle);
        }
    }

    private void UpdateMovementState() 
    {
        Play(isRunning ? _Running : _Walking);
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
        var state = Play(_UsingHoe);
        state.Events.OnEnd = () => isUsingTool = false;
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
