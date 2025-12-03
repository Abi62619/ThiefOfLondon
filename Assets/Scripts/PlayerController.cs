using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerNew : MonoBehaviour
{
    public InputActionAsset inputActionAsset;

    private InputAction moveAction;
    private InputAction interactAction;
    private InputAction jumpAction;

    [Header("Movement")]
    private Vector2 moveInput;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    [Header("Jump")]
    [SerializeField] float jumpForce = 10;
    [SerializeField] private Rigidbody rb;
    private bool isGrounded = true; 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked; //can be deleted 
        Cursor.visible = false;

        MovePlayer();
        RotatePlayerTowardMovement();
    }

    private void OnEnable()
    {
        var actionMap = inputActionAsset.FindActionMap("Player");
        moveAction = actionMap.FindAction("Move");
        interactAction = actionMap.FindAction("Interact");
        jumpAction = actionMap.FindAction("Jump");

        moveAction.Enable();
        interactAction.Enable();
        jumpAction.Enable();

        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
        interactAction.performed += OnInteract;
        jumpAction.performed += OnJump;
    }

    private void OnDisable()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
        interactAction.performed -= OnInteract;
        jumpAction.performed -= OnJump;

        moveAction.Disable();
        interactAction.Disable();
        jumpAction.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interact button pressed!");
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded) // context performed input triggered 
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        Debug.Log("You Jumped");
    }

    private void MovePlayer()
    {
        Vector3 move = new Vector3(-moveInput.x, 0, -moveInput.y);

        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }

    private void RotatePlayerTowardMovement()
    {
        if (moveInput.sqrMagnitude > 0.01f)
        {
            Vector3 direction = new Vector3(-moveInput.x, 0, -moveInput.y);

            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}