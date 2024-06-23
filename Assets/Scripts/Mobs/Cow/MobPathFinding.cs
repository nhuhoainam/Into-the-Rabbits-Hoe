using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobPathFinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private Vector2 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirection = Vector2.zero;
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection.normalized * moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            moveDirection = Vector2.zero;
        }
    }

    public void Stop()
    {
        moveDirection = Vector2.zero;
    }

    public void MoveTo(Vector2 targetPosition)
    {
        this.targetPosition = targetPosition;
        moveDirection = targetPosition - (Vector2)transform.position;
    }

    public bool IsMoving()
    {
        return moveDirection != Vector2.zero;
    }
}
