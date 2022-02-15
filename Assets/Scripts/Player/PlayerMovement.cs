using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;

    private PlayerControls controls;
    private PlayerControls.MovementActions movement;

    private Rigidbody2D rb;

    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
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
        moveInput = movement.Move.ReadValue<Vector2>();
        rb.velocity = moveInput * movementSpeed;
    }
}
