using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_OverworldPlayerMovement : MonoBehaviour
{
    public bool CanMove;

    //Variables for Controls
    private Vector2 MoveInput;
    private Vector2 moveDirection;

    //Speed Variables
    public float moveSpeed = 5f;
    private float currentSpeed;
    public Rigidbody2D rb;

    private void Start()
    {
        CanMove = true;
    }

    private void Update()
    {
        MoveInput.x = Input.GetAxisRaw("Horizontal");
        MoveInput.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if(CanMove == true)
        {
            moveDirection = MoveInput.normalized;

            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
