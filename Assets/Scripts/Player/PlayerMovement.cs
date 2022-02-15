using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;

    public bool shouldMove = true;

    private Animator animator;
    private PlayerControls controls;
    private PlayerControls.MovementActions movement;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private static readonly int IsMoving = Animator.StringToHash("isMoving");

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        controls = new PlayerControls();
        movement = controls.Movement;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void FixedUpdate()
    {
        if (shouldMove)
        {
            moveInput = movement.Move.ReadValue<Vector2>();
            rb.velocity = moveInput * movementSpeed;

            animator.SetBool(IsMoving, moveInput == Vector2.zero);
        }
    }
}
