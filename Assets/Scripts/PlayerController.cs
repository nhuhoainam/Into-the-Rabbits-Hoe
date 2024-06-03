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

    private PlayerControls playerControls;
    private Rigidbody2D playerRb;
    private Vector2 movement;

    private bool isRunning = false;

    private bool isUsingTool = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        playerControls.Player.Run.performed += ctx => isRunning = true;
        playerControls.Player.Run.canceled += ctx => isRunning = false;
        playerControls.Player.Interact.performed += ctx => Interact();
    }
    
    private AnimancerState Play(DirectionalAnimationSet animations)
    {
        var clip = animations.GetClip(_Direction);
        return _Animancer.Play(clip);
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
        movement = playerControls.Player.Move.ReadValue<Vector2>();

        if (movement != Vector2.zero)
        {
            _Direction = movement.normalized;
            if (isRunning)
                Play(_Running);
            else
                Play(_Walking);
        }
        else
        {
            Play(_Idle);
        }
    }

    private void MovePlayer()
    {
        playerRb.MovePosition(playerRb.position 
            + movement 
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
