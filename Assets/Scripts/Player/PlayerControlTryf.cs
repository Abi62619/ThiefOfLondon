using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerTryf : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionAsset inputActionAsset;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction lookAction;
    private InputAction crouchAction;
    private InputAction slideAction;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Vector2 moveInput;
    private Vector2 lookInput;

    [Header("Mouse Look Settings")]
    public float mouseSense = 2f;
    public Transform playerCamera;
    private float rotationX = 0f;

    [Header("Crouch & Slide Settings")]
    private Vector3 crouchScale = new Vector3(1f, 0.25f, 1f);
    private Vector3 playerScale = new Vector3(1f, 1f, 1f);

    public float slideSpeed = 10f;
    public float slideDuration = 1f;
    private bool isSliding = false;
    private float slideTimer = 0f;

    private Rigidbody rb;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        var actionMap = inputActionAsset.FindActionMap("Player");

        moveAction = actionMap.FindAction("Move");
        jumpAction = actionMap.FindAction("Jump");
        lookAction = actionMap.FindAction("Look");
        crouchAction = actionMap.FindAction("Crouch");
        slideAction = actionMap.FindAction("Slide");

        moveAction?.Enable();
        jumpAction?.Enable();
        lookAction?.Enable();
        crouchAction?.Enable();
        slideAction?.Enable();

        jumpAction.performed += OnJump;
        crouchAction.started += OnCrouchStarted;
        crouchAction.canceled += OnCrouchCanceled;
        slideAction.started += OnSlideStarted;
    }

    void OnDisable()
    {
        jumpAction.performed -= OnJump;
        crouchAction.started -= OnCrouchStarted;
        crouchAction.canceled -= OnCrouchCanceled;
        slideAction.started -= OnSlideStarted;

        moveAction?.Disable();
        jumpAction?.Disable();
        lookAction?.Disable();
        crouchAction?.Disable();
        slideAction?.Disable();
    }

    void Update()
    {
        // Look
        lookInput = lookAction.ReadValue<Vector2>() * mouseSense;
        rotationX -= lookInput.y;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * lookInput.x);

        // Sliding logic
        if (isSliding)
        {
            slideTimer += Time.deltaTime;
            if (slideTimer >= slideDuration)
                EndSlide();

            Vector3 slideMove = transform.forward * slideSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + slideMove);
        }
    }

    void FixedUpdate()
    {
        if (!isSliding)
        {
            moveInput = moveAction.ReadValue<Vector2>();
            Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
            Vector3 targetVelocity = move * moveSpeed;

            Vector3 velocity = rb.linearVelocity;
            Vector3 velocityChange = targetVelocity - new Vector3(velocity.x, 0, velocity.z);

            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded && !isSliding)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCrouchStarted(InputAction.CallbackContext context)
    {
        Debug.Log("Crouch started, position: " + transform.position); 
        if (!isSliding)
        {
            transform.localScale = crouchScale;
            transform.position -= new Vector3(0, 0.5f, 0);
        }
    }

    private void OnCrouchCanceled(InputAction.CallbackContext context)
    {
        if (!isSliding)
        {
            transform.localScale = playerScale;
            transform.position += new Vector3(0, 0.5f, 0);
        }
    }

    private void OnSlideStarted(InputAction.CallbackContext context)
    {
        Debug.Log("Slide started, position: " + transform.position);
        if (!isSliding)
        {
            moveInput = moveAction.ReadValue<Vector2>();
            if (moveInput.magnitude > 0.1f && isGrounded)
            {
                isSliding = true;
                slideTimer = 0f;

                // Shrink player like crouch
                transform.localScale = crouchScale;
                transform.position -= new Vector3(0, 0.5f, 0);
            }
        }
    }

    private void EndSlide()
    {
        isSliding = false;
        transform.localScale = playerScale;
        transform.position += new Vector3(0, 0.5f, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}