using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using Synty.AnimationBaseLocomotion.Samples;

public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    public InputActionAsset inputActionAsset;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction lookAction;
    private InputAction crouchAction;
    private InputAction slideAction;

    [Header("Player Settings")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private CapsuleCollider cc;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform playerObj;
    [SerializeField] private Transform playerCamera;

    [Header("Movement Settings")]
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private float moveSpeed = 6f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 5f; 
    [HideInInspector] private bool isGrounded; 

    [Header("Mouse Look Settings")]
    [SerializeField] private float mouseSpeed = 2f;
    [SerializeField] private Vector2 lookInput;
    [HideInInspector] private float rotationX;

    [Header("Crouch Settings")]
    [SerializeField] private float crouchHeight = 1f;
    [HideInInspector] private float standingHeight;
    [HideInInspector] private bool isCrouching;

    [Header("Slide Settings")]
    [SerializeField] private float slideForce = 12f;
    [SerializeField] private float maxSlideTime = 1f;
    [SerializeField] private float slideYScale = 0.5f;
    [HideInInspector] private bool isSliding;
    [HideInInspector] private float slideTimer;
    [HideInInspector] private float startYScale;

    //Animation Settings 
    [HideInInspector] private Animator anim; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();

        standingHeight = cc.height;
        startYScale = playerObj.localScale.y;
    }

    void Update()
    {
        MouseLook();
        CCHeight();
        HandleSlide();  
        UpdateAnimations(); 
    }

    void FixedUpdate()
    {
        Movement();

        if (isSliding)
            SlidingMovement();
    }

    // ================= INPUT SETUP =================

    void OnEnable()
    {
        var actionMap = inputActionAsset.FindActionMap("Player");

        moveAction = actionMap.FindAction("Move");
        jumpAction = actionMap.FindAction("Jump");
        lookAction = actionMap.FindAction("Look");
        crouchAction = actionMap.FindAction("Crouch");
        slideAction = actionMap.FindAction("Slide");

        actionMap.Enable();

        moveAction.Enable(); 
        jumpAction.Enable(); 
        lookAction.Enable(); 
        crouchAction.Enable(); 
        slideAction.Enable(); 

        jumpAction.performed += OnJump;
        crouchAction.performed += OnCrouch;
        crouchAction.canceled += OnCrouch; 

        // slide start + stop
        slideAction.started += OnSlideStart;
        slideAction.canceled += OnSlideStop;
    }

    void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        lookAction.Disable();
        crouchAction.Disable(); 
        slideAction.Disable(); 

        jumpAction.performed -= OnJump;
        crouchAction.performed -= OnCrouch;
        crouchAction.canceled -= OnCrouch; 
        slideAction.started -= OnSlideStart;
        slideAction.canceled -= OnSlideStop;
    }

    // ================= MOVEMENT =================

    void Movement()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        Vector3 move = orientation.forward * moveInput.y +
                       orientation.right * moveInput.x;

        Vector3 targetVelocity = move * moveSpeed;

        Vector3 velocity = rb.linearVelocity;

        Vector3 velocityChange =
            targetVelocity - new Vector3(velocity.x, 0, velocity.z);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    // ================= MOUSE LOOK =================

    void MouseLook()
    {
        lookInput = lookAction.ReadValue<Vector2>() * mouseSpeed;

        rotationX -= lookInput.y;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        playerCamera.localRotation =
            Quaternion.Euler(rotationX, 0f, 0f);

        transform.Rotate(Vector3.up * lookInput.x);
    }

    // ================= JUMP =================

    void OnJump(InputAction.CallbackContext context)
    {
        if (!isGrounded) return;

        Debug.Log("Jump working"); 

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        anim.SetTrigger("Jump"); 
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; 
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    // ================= CROUCH =================

    void OnCrouch(InputAction.CallbackContext context)
    {
        isCrouching = context.ReadValueAsButton();
    }

    void CCHeight()
    {
        cc.height = isCrouching ? crouchHeight : standingHeight;
    }

    // ================= SLIDE =================

    void OnSlideStart(InputAction.CallbackContext context)
    {
        if (moveInput != Vector2.zero)
            StartSlide();
    }

    void OnSlideStop(InputAction.CallbackContext context)
    {
        StopSlide();
    }

    void StartSlide()
    {
        isSliding = true;
        slideTimer = maxSlideTime;

        playerObj.localScale =
            new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);

        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
    }

    void HandleSlide()
    {
        if (!isSliding) return;

        slideTimer -= Time.deltaTime;

        if (slideTimer <= 0f)
            StopSlide();
    }

    void SlidingMovement()
    {
        Vector3 inputDirection =
            orientation.forward * moveInput.y +
            orientation.right * moveInput.x;

        rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);
    }

    void StopSlide()
    {
        if (!isSliding) return;

        isSliding = false;

        playerObj.localScale =
            new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
    }

    //================== ANIMATION =================
    void UpdateAnimations()
    {
        bool isMoving = moveInput.magnitude > 0.1f; 

        anim.SetBool("isWalking", isMoving && isGrounded && !isSliding); 
        anim.SetBool("isGrounded", isGrounded); 
        anim.SetBool("isCrouching", isCrouching); 
        anim.SetBool("isSliding", isSliding); 
    }
}