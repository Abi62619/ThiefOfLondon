using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlTryf : MonoBehaviour
{
    public InputActionAsset inputActionAsset;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction lookAction;

    public Vector2 moveInput;
    public Vector2 lookInput;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("Mouse Look Settings")]
    public float mouseSense = 2f;
    public Transform playerCamera;

    private Rigidbody rb;
    private float rotationX = 0f;
    private bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();



    }
    private void OnEnable()
    {
        var actionMap = inputActionAsset.FindActionMap("Player");
        moveAction = actionMap.FindAction("Move");
        jumpAction = actionMap.FindAction("Jump");
        lookAction = actionMap.FindAction("Look");

        moveAction.Enable();
        jumpAction.Enable();
        lookAction.Enable();

        jumpAction.performed += OnJump;
      
    }

    private void OnDisable()
    {
   
        jumpAction.performed -= OnJump;
       

        moveAction.Disable();
        jumpAction.Disable();
        lookAction.Disable();
    }
   

    

    private void OnJump(InputAction.CallbackContext context)
    {
       
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

    }


    // Update is called once per frame
    void Update()
    {
       
        lookInput = lookAction.ReadValue<Vector2>()*mouseSense;
        rotationX -= lookInput.y;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * lookInput.x);

     
    }

    private void FixedUpdate()
    {

        moveInput = moveAction.ReadValue<Vector2>();
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        Vector3 targetVelocity = move * moveSpeed;
        Vector3 velocity = rb.linearVelocity;
        Vector3 velocityChange = targetVelocity - new Vector3(velocity.x, 0, velocity.z);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);

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
