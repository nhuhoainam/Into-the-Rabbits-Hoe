using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    public Rigidbody2D rb = null;

    Vector2 movement;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
