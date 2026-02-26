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
    private InputAction sprintAction; 

    [Header("Player Settings")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private CapsuleCollider cc;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform playerObj;
    [SerializeField] private Transform playerCamera;

    [Header("Movement Settings")]
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float sprintSpeed = 10f; 
    [HideInInspector] private bool isSprinting; 

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 5f; 
    [HideInInspector] private bool isGrounded; 

    [Header("Mouse Look Settings")]
    [SerializeField] private float mouseSpeed = 1f;
    [SerializeField] private Vector2 input; 
    [SerializeField] private Transform cameraTransform; 
    [HideInInspector] private float turnSmoothVelocity; 
    [HideInInspector] private float rotationX;

    [Header("Crouch Settings")]
    [SerializeField] private float crouchHeight = 2f;
    [HideInInspector] private float standingHeight;
    [HideInInspector] private bool isCrouching;

    [Header("Slide Settings")]
    [SerializeField] private float slideForce = 12f;
    [SerializeField] private float maxSlideTime = 1f;
    [SerializeField] private float slideYScale = 0.5f;
    [HideInInspector] private bool isSliding;
    [HideInInspector] private float slideTimer;
    [HideInInspector] private float startYScale;

    [Header("Animation Settings")] 
    [SerializeField] private Animator playerAnim;
    [SerializeField] private float idleTimeBeforeStop = 0f;
    [HideInInspector] private float idleTimer; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        playerAnim = GetComponent<Animator>(); 

        standingHeight = cc.height;
        startYScale = playerObj.localScale.y;
    }

    void Update()
    {
        MouseLook();
        CCHeight();
        HandleSlide();  
    }

    void FixedUpdate()
    {
        Movement();

        if (isSliding)
            SlidingMovement();
    }

    #region Input Setup 

    void OnEnable()
    {
        var actionMap = inputActionAsset.FindActionMap("Player");

        moveAction = actionMap.FindAction("Move");
        jumpAction = actionMap.FindAction("Jump");
        lookAction = actionMap.FindAction("Look");
        crouchAction = actionMap.FindAction("Crouch");
        slideAction = actionMap.FindAction("Slide");
        sprintAction = actionMap.FindAction("Sprint"); 

        actionMap.Enable();

        moveAction.Enable(); 
        jumpAction.Enable(); 
        lookAction.Enable(); 
        crouchAction.Enable(); 
        slideAction.Enable(); 
        sprintAction.Enable(); 

        // jump start 
        jumpAction.performed += OnJump;

        //crouch start + stop 
        crouchAction.performed += OnCrouch;
        crouchAction.canceled += OnCrouch; 

        // slide start + stop
        slideAction.started += OnSlideStart;
        slideAction.canceled += OnSlideStop;

        // sprint start + stop 
        sprintAction.performed += OnSprint; 
        sprintAction.canceled += OnSprint; 
    }

    void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        lookAction.Disable();
        crouchAction.Disable(); 
        slideAction.Disable(); 
        sprintAction.Disable(); 

        // jump start 
        jumpAction.performed -= OnJump;

        //crouch start + stop 
        crouchAction.performed -= OnCrouch;
        crouchAction.canceled -= OnCrouch; 

        // slide start + stop
        slideAction.started -= OnSlideStart;
        slideAction.canceled -= OnSlideStop;

        // sprint start + stop 
        sprintAction.performed -= OnSprint; 
        sprintAction.canceled -= OnSprint; 
    }

    #endregion
    #region Movement 

    void Movement()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        Vector3 move = orientation.forward * moveInput.y +
                    orientation.right * moveInput.x;

        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;
        Vector3 targetVelocity = move * currentSpeed;

        Vector3 velocity = rb.linearVelocity;

        Vector3 velocityChange =
            targetVelocity - new Vector3(velocity.x, 0, velocity.z);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);

        // ===== ANIMATION LOGIC =====

        if (moveInput != Vector2.zero)
        {
            playerAnim.SetBool("isWalking", true);
            idleTimer = 0f; // reset timer when moving
        }
        else
        {
            idleTimer += Time.deltaTime;

            if (idleTimer <= idleTimeBeforeStop)
            {
                playerAnim.SetBool("isWalking", false);
            }
        }
    }

    void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.ReadValueAsButton();
        playerAnim.SetBool("isSprinting", isSprinting);
    }

    #endregion
    #region mouseLook 

    void MouseLook()
    {
        Vector3 moveDir = new Vector3(input.x, 0f, input.y).normalized;

        if (moveDir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.1f);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
        }
    }

    #region Jump

    void OnJump(InputAction.CallbackContext context)
    {
        if (!isGrounded) return;

        Debug.Log("Jump working"); 

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        if (moveInput != Vector2.zero)
            StartCoroutine(WalkingJumpRoutine());
        else
            StartCoroutine(IdleJumpRoutine());
    }

    IEnumerator IdleJumpRoutine()
    {
        playerAnim.SetBool("isUp_I_Jump", true);

        yield return new WaitForSeconds(0.5f); // match jump anim length

        playerAnim.SetBool("isUp_I_Jump", false);
        playerAnim.SetBool("isDn_I_Jump", true);
    }

    IEnumerator WalkingJumpRoutine()
    {
        playerAnim.SetBool("isUp_W_Jump", true); 

         //wait for the length of current animation 
        yield return new WaitForSeconds(
            playerAnim.GetCurrentAnimatorStateInfo(0).length
        ); 

        playerAnim.SetBool("isUp_W_Jump", false); 
        playerAnim.SetBool("isDn_W_Jump", true); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            playerAnim.SetBool("isDn_W_Jump", false); 
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    #endregion
    #region Crouch

    void OnCrouch(InputAction.CallbackContext context)
    {
        isCrouching = context.ReadValueAsButton();

        playerAnim.SetBool("Crouch", isCrouching); 
        playerAnim.SetBool("IdleCrouch", isCrouching); 
    }

    void CCHeight()
    {
        cc.height = isCrouching ? crouchHeight : standingHeight;
    }

    #endregion
    #region Slide

    void OnSlideStart(InputAction.CallbackContext context)
    {
        if (moveInput != Vector2.zero)
            StartSlide();

        //playerAnim.SetBool("Slide"); 
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

    #endregion
}   