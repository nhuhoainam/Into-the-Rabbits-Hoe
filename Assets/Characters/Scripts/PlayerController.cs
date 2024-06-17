using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private AudioSource footstepAudioSource;
    [SerializeField] private AudioClip footstepSound;
    [SerializeField] private float stepInterval = 0.5f;
    [SerializeField] private float footstepVolume = 30.0f; 

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private float stepTimer = 0f;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        // Ensure the footstepAudioSource is properly set up
        if (footstepAudioSource == null)
        {
            footstepAudioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set the volume to the desired level
        footstepAudioSource.volume = footstepVolume;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        PlayerInput();
        AdjustSpriteDirection();
        HandleFootstepSounds();
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

    private void AdjustSpriteDirection()
    {
        bool isRunning = movement.sqrMagnitude > 0;
        myAnimator.SetBool("isRunning", isRunning);

        if (isRunning)
        {
            if (movement.x > 0)
            {
                myAnimator.SetInteger("side", 4);
            }
            else if (movement.x < 0)
            {
                myAnimator.SetInteger("side", 3);
            }
            else if (movement.y > 0 && (movement.x == 0 || movement.x > 0))
            {
                myAnimator.SetInteger("side", 2);
            }
            else if (movement.y < 0 && (movement.x == 0 || movement.x > 0))
            {
                myAnimator.SetInteger("side", 1);
            }
        }
    }

    private void HandleFootstepSounds()
    {
        if (movement.sqrMagnitude > 0)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayFootstepSound();
                stepTimer = stepInterval;
            }
        }
        else
        {
            StopFootstepSound();
            stepTimer = 0f;
        }
    }

    private void PlayFootstepSound()
    {
        if (footstepSound != null && !footstepAudioSource.isPlaying)
        {
            footstepAudioSource.clip = footstepSound;
            footstepAudioSource.loop = true;
            footstepAudioSource.Play();
        }
    }

    private void StopFootstepSound()
    {
        if (footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Stop();
        }
    }
}
