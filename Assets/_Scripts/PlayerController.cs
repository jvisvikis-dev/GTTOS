using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Unity.Cinemachine;

public class PlayerController : MonoBehaviour

{ 
    [Header("References")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private CinemachineCamera cam;
    [Header("MoveSettings")]
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [Header("LookSettings")]
    [SerializeField] private float maxLookAngle = 90f;
    [SerializeField] private float turnSpeed = 0.5f;

    private Controls _inputActions;
    private float verticalRotation = 0f;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        _inputActions = new Controls();
        //_inputActions.Player.Jump.performed += Jump;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private Vector3 GetPlayerMovement()
    {
        Vector2 values = _inputActions.Player.Move.ReadValue<Vector2>();
        Vector3 direction = new Vector3(values.x,0,values.y);
        Vector3 move = direction.z*transform.forward + transform.right*direction.x;
        move = Vector3.ClampMagnitude(move, 1f);
        return move;
    }

    private void HandleMovement()
    {
        bool isGrounded = IsGrounded();
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        Vector3 move = GetPlayerMovement();
        controller.Move(move * Time.deltaTime * speed);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        
    }

    private void HandleRotation()
    {
        Vector2 values = _inputActions.Player.MouseLook.ReadValue<Vector2>() * turnSpeed;
        verticalRotation -= values.y;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxLookAngle, maxLookAngle);
        cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        transform.Rotate(Vector3.up * values.x);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        bool isGrounded = IsGrounded();
        if(isGrounded)
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.03f, Vector3.down, groundCheckDistance);
    }
}
