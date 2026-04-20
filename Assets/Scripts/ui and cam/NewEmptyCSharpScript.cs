using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerControllerNewNew : MonoBehaviour
{
    [Header("Input System")]
    public InputActionAsset inputActionAsset;

    private InputAction moveAction;
    private InputAction interactAction;
    private InputAction jumpAction;

    private Vector2 moveInput;

    [Header("Movement")]
    public float moveSpeed = 5f;          // Forward movement speed
    public float rotationSpeed = 120f;    // Degrees per second for A/D rotation

    [Header("Jumping")]
    public float jumpHeight = 2f;          // EXACT jump height in Unity units
    public float gravity = -9.81f;         // Gravity acceleration (negative)

    [Header("Ground Check")]
    public Transform groundCheck;          // Empty transform at feet
    public float groundCheckRadius = 0.2f; // Radius of overlap sphere
    public LayerMask groundLayer;          // What counts as ground

    private CharacterController controller;
    private float verticalVelocity;        // Current vertical speed
    private bool isGrounded;                // Cached grounded state
    private bool wasGrounded;
    private int jumpCount = 0;

    private void Awake()
    {
        // Cache CharacterController reference (faster + cleaner)
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        // Fetch the Player action map
        var actionMap = inputActionAsset.FindActionMap("Player");

        // Grab individual actions
        moveAction = actionMap.FindAction("Move");
        interactAction = actionMap.FindAction("Interact");
        jumpAction = actionMap.FindAction("Jump");

        // Enable them so they start listening for input
        moveAction.Enable();
        interactAction.Enable();
        jumpAction.Enable();

        // Subscribe to input callbacks
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
        interactAction.performed += OnInteract;
        jumpAction.performed += OnJump;
    }

    private void OnDisable()
    {
        // Always unsubscribe to avoid memory leaks / double calls
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
        jumpAction.performed -= OnJump;

        moveAction.Disable();
        interactAction.Disable();
        jumpAction.Disable();
    }

    private void Update()
    {
        if (isGrounded && !wasGrounded)
        {
            // player just landed
            jumpCount = 0;
        }

        wasGrounded = isGrounded;
        // Order matters:
        // 1. Check if grounded
        // 2. Rotate
        // 3. Move horizontally
        // 4. Apply gravity & vertical motion
        CheckGrounded();
        HandleRotation();
        HandleMovement();
        ApplyGravity();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        // Reads WASD / stick input as a Vector2
        // X = A/D, Y = W/S
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interact button pressed!");

        //Dialog.Instance.OnTalkies();
        
    }

    bool CanDoubleJump()
    {
        return PlayerPrefs.GetInt("PlayerHasPHat", 0) == 1;
        
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) 
            return;

        int maxJumps = CanDoubleJump() ? 2 : 1;

        if (jumpCount >= maxJumps)
            return;

        verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        jumpCount++;
    }

    private void HandleMovement()
    {
        // Only move forward when pressing W (positive Y)
        if (moveInput.y > 0f)
        {
            // Character moves in its own forward direction
            Vector3 move = transform.forward * moveSpeed;

            // CharacterController handles collision internally
            controller.Move(move * Time.deltaTime);
        }
    }

    private void HandleRotation()
    {
        // A/D input directly rotates the character
        float rotationInput = moveInput.x;

        // Convert input into degrees per frame
        float rotationAmount = rotationInput * rotationSpeed * Time.deltaTime;

        transform.Rotate(0f, rotationAmount, 0f);
    }

    private void ApplyGravity()
    {
        /*
         * If grounded and falling, clamp velocity slightly downward.
         * This prevents the CharacterController from "hovering"
         * and ensures consistent ground contact.
         */
        if (isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        // Gravity accelerates the character downward every frame
        verticalVelocity += gravity * Time.deltaTime;

        // Apply vertical movement
        Vector3 verticalMove = Vector3.up * verticalVelocity;
        controller.Move(verticalMove * Time.deltaTime);
    }

    private void CheckGrounded()
    {
    
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the ground check sphere in the editor
        if (groundCheck == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
