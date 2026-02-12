using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Input Information")]

    // Reference to the Input System action asset
    public InputActionAsset inputActionAsset;

    // Stores current movement input 
    public Vector2 moveInput;

    // Stores current mouse look input
    public Vector2 lookInput;

    // INPUT ACTIONS
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction lookAction;
    private InputAction crouchAction;
    private InputAction slideAction;

    [Header("Player Settings")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private CapsuleCollider cc;

    [Header("Movement Settings")]

    // Movement speed multiplier
    [SerializeField] private float moveSpeed = 5f;

    // Force applied when jumping
    [SerializeField] private float jumpForce = 5f;
    // To make Grounded work 
    [SerializeField] private LayerMask groundLayer; 
    [SerializeField] private float groundCheckDistance = 0.2f;  
    [SerializeField] private float rayDistance = 0.2f;

    [Header("Mouse Look Settings")]

    // Mouse sensitivity
    [SerializeField] private float mouseSpeed = 2f;

    // Stores vertical camera rotation
    [SerializeField] private float rotationX;

    // Reference to camera transform
    [SerializeField] private Transform playerCamera;

    [Header("Crouch Settings")]

    // Collider height while crouching
    [SerializeField] private float crouchHeight = 1f;

    // Collider height while standing
    [SerializeField] private float standingHeight = 2f;

    [Header("Slide Settings")]
    // Extra forward force applied when slide starts
    [SerializeField] private float slideForce = 10f;

    // How long the slide lasts
    [SerializeField] private float slideDuration = 0.8f;

    // Timer used to count down slide duration
    private float slideTimer;

    // State flags
    private bool isGrounded;
    private bool isCrouching;
    private bool isSliding;

    //Temporary 
    Color color; 

    void Start()
    {
        // Get references 
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();

        // Store original standing height from collider
        standingHeight = cc.height;

        //Temporary 
        GetComponent<SpriteRenderer>().color = color.yellow; 
    }

    void Update()
    {
        // Handle camera rotation
        MouseLook();

        // Adjust collider height based on crouch state
        CCcrouchHeight();

        // Handle slide timer logic
        Slide();
    }

    void FixedUpdate()
    {
        // Physics-based movement happens in FixedUpdate
        Movement();
    }

    void OnEnable()
    {
        //Tempapory Debugs
        if(inputActionAsset == null)
        {
            Debug.LogError("InputActionAsset not assigned!"); 
            return; 
        }

        // Get the Player action map from the Input Action Asset
        var actionMap = inputActionAsset.FindActionMap("Player");
        actionMap.Enable(); 
    
        // Find each action by name
        moveAction = actionMap.FindAction("Move");
        jumpAction = actionMap.FindAction("Jump");
        lookAction = actionMap.FindAction("Look");
        crouchAction = actionMap.FindAction("Crouch");
        slideAction = actionMap.FindAction("Slide");

        // Enable input actions
        moveAction.Enable();
        jumpAction.Enable();
        lookAction.Enable();
        crouchAction.Enable();
        slideAction.Enable();

        // Subscribe to input events
        jumpAction.performed += OnJump;
        crouchAction.performed += OnCrouch;
        slideAction.performed += OnSlide;

        Debug.Log(crouchAction != null? "Crouch action found" : "Crouch action Missing"); 
        Debug.Log(slideAction != null? "Slide action found" : "Slide Action Missing"); 
        Debug.Log($"Action map found: {actionMap.name}"); 
    }

    void OnDisable()
    {
        // Disable input actions
        moveAction.Disable();
        jumpAction.Disable();
        lookAction.Disable();
        crouchAction.Disable();
        slideAction.Disable();

        // Unsubscribe from input events
        jumpAction.performed -= OnJump;
        crouchAction.performed -= OnCrouch;
        slideAction.performed -= OnSlide;
    }

    // JUMP
    void OnJump(InputAction.CallbackContext context)
    {
        if(isGrounded())
        {
            // Apply upward force instantly
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            if(!isGrounded) return;
        }
        Debugging(); 
    }

    bool isGrounded()
    {
        int LayerMask = 1 << 8; 
        return Physics.Raycast(transform.position, new Vector3(0, -rayDistance, 0), rayDistance, LayerMask); 
    }

    void Debugging()
    {
        Debug.DrawRay(transform.position, new Vector3(0, -rayDistance, 0), color.yellow);
    }

    void OnCollisionStay(Collision collision)
    {
        // Assume grounded when colliding with something
        isGrounded = true;
    }

    // PLAYER MOVEMENT
    void Movement()
    {
        // Read movement input (Vector2 from input system)
        moveInput = moveAction.ReadValue<Vector2>();

        // Convert input into world-space movement direction
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        // Desired movement velocity
        Vector3 targetVelocity = move * moveSpeed;

        // Current rigidbody velocity
        Vector3 velocity = rb.linearVelocity;

        // Calculate difference between desired and current velocity (ignore vertical)
        Vector3 velocityChange = targetVelocity - new Vector3(velocity.x, 0, velocity.z);

        // Apply instant velocity change for responsive movement
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    void MouseLook()
    {
        // Read mouse input and apply sensitivity
        lookInput = lookAction.ReadValue<Vector2>() * mouseSpeed;

        // Vertical rotation (look up/down)
        rotationX -= lookInput.y;

        // Limit vertical camera angle
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // Apply vertical rotation to camera
        playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        // Rotate player horizontally (turn left/right)
        transform.Rotate(Vector3.up * lookInput.x);
    }

    // CROUCH
    void OnCrouch(InputAction.CallbackContext context)
    {
        // Set crouch state based on button press
        isCrouching = context.ReadValueAsButton();

        //temp debug 
        Debug.Log($"Crouch phase: {context.phase}");
    }

    void CCcrouchHeight()
    {
        // Change collider height depending on crouch state
        cc.height = isCrouching ? crouchHeight : standingHeight;
    }

    // SLIDE
    void OnSlide(InputAction.CallbackContext context)
    {
        // Check for slide start conditions
        if(context.performed && isGrounded && moveInput.y > 0)
        {
            isSliding = true;

            // Start slide timer
            slideTimer = slideDuration;

            // Force crouch during slide
            isCrouching = true;

            // Apply forward velocity boost
            rb.AddForce(transform.forward * slideForce, ForceMode.VelocityChange);
        }

        //temp debug 
        Debug.Log($"Slide phase: {context.phase}");
    }

    void Slide()
    {
        // If not sliding, exit
        if(!isSliding) return;

        // Reduce timer each frame
        slideTimer -= Time.deltaTime;

        // End slide when timer reaches zero
        if(slideTimer <= 0)
        {
            isSliding = false;
            isCrouching = false;
        }
    }
}