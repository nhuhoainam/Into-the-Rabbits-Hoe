using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;

    const string PLAYER_IDLE = "Player-Idle-";
    const string PLAYER_WALK = "Player-Walking-";
    const string PLAYER_RUN = "Player-Running-";
    const string PLAYER_USE_HOE = "Player-Using-Hoe-";
    const string PLAYER_USE_AXE = "Player-Using-Axe-";
    const string PLAYER_USE_WATERING_CAN = "Player-Using-Watering-Can-";

    const string DIRECTION_FRONT = "Front";
    const string DIRECTION_BACK = "Back";
    const string DIRECTION_LEFT = "Left";
    const string DIRECTION_RIGHT = "Right";

    private PlayerControls playerControls;
    private Rigidbody2D playerRb;
    private Vector2 movement;

    Animator mAnimator;
    private string mCurrentAnim;
    private string mCurrentDirection;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerRb = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        mCurrentAnim = PLAYER_IDLE;
        mCurrentDirection = DIRECTION_FRONT;
    }

    private void Start()
    {
    }

    void ChangeAnimationState(string newState, string newDirection)
    {
        if (mCurrentAnim == newState && mCurrentDirection == newDirection) return;

        mAnimator.Play(newState + newDirection);

        mCurrentAnim = newState;
        mCurrentDirection = newDirection;
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
        movement = playerControls.Player.Move.ReadValue<Vector2>();
        if (movement.x > 0)
        {
            ChangeAnimationState(PLAYER_WALK, DIRECTION_RIGHT);
        }
        else if (movement.x < 0)
        {
            ChangeAnimationState(PLAYER_WALK, DIRECTION_LEFT);
        }
        else if (movement.y > 0)
        {
            ChangeAnimationState(PLAYER_WALK, DIRECTION_BACK);
        }
        else if (movement.y < 0)
        {
            ChangeAnimationState(PLAYER_WALK, DIRECTION_FRONT);
        }
        else
        {
            ChangeAnimationState(PLAYER_IDLE, mCurrentDirection);
        }
    }

    private void MovePlayer()
    {
        playerRb.MovePosition(playerRb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void Interact()
    {
        ChangeAnimationState(PLAYER_USE_HOE, mCurrentDirection);
    }

    private void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            Interact();
        }
    }

    private void Idle()
    {
        ChangeAnimationState(PLAYER_IDLE, mCurrentDirection);
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
