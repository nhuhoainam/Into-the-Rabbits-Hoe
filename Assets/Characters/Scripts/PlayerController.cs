using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;

    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
        AdjustSpriteDirection();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void AdjustSpriteDirection() {
        bool isRunning = movement.sqrMagnitude > 0;
        myAnimator.SetBool("isRunning", isRunning);

        if (isRunning) {
            if (movement.x > 0) {
                myAnimator.SetInteger("side", 4);
            } else if (movement.x < 0) {
                myAnimator.SetInteger("side", 3);
            } else if (movement.y > 0 && (movement.x == 0 || movement.x > 0)) {
                myAnimator.SetInteger("side", 2);
            } else if (movement.y < 0 && (movement.x == 0 || movement.x > 0)) {
                myAnimator.SetInteger("side", 1);
            }
        }
    }
}
