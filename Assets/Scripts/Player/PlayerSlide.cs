using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerSlide : MonoBehaviour
{
    [Header("Input Actions")]
    public PlayerControlTryf playerScript;
    public InputActionAsset inputActionAsset;
    private InputAction slideAction;

    [Header("Player References")]
    public Transform orientation;
    public Transform playerobj;
    private Rigidbody rb;
    private float rotationX = 0f;
    private bool isGrounded;

    [Header("Sliding")]
    [SerializeField] private float maxSlideTime;
    [SerializeField] private float slideForce;
    [SerializeField] private float slideTimer;
    [SerializeField] private float slideYScale;
    private float startYScale;
    private bool isSliding;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerScript = GetComponent<PlayerControlTryf>();

        startYScale = playerobj.localScale.y;
    }

    private void Update()
    {
        HandleMovement();
        StartSlide();
    }

    private void FixedUpdate()
    {
        if (isSliding)
        {
            SlidingMovement(); 
        }
    }

    private void HandleMovement()
    {

    }

    private void StartSlide()
    {
        isSliding = true; 

        playerobj.localScale = new Vector3(playerobj.localScale.x, slideYScale, playerobj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    private void SlidingMovement()
    {

    }

    private void OnEnable()
    {
        Debug.Log("Crouch can happen");

        if (inputActionAsset == null)
        {
            Debug.LogError("InputActionAsset NOT assigned!");
            return;
        }

        var actionMap = inputActionAsset.FindActionMap("Player");
        if (actionMap == null) return;

        slideAction = actionMap.FindAction("Slide");
        if (slideAction == null) return;

        slideAction.Enable();
        slideAction.performed += OnSlide;
        slideAction.canceled += OnSlide;

        Debug.Log(slideAction != null ? "Slide action found" : "Slide action MISSING");
        Debug.Log($"Action map found: {actionMap.name}");
    }

    private void OnDisable()
    {
        slideAction.performed -= OnSlide;
        slideAction.canceled -= OnSlide;

        slideAction.Disable();
    }

    private void OnSlide(InputAction.CallbackContext context)
    {
        Debug.Log($"Slide phase: {context.phase}");
        isSliding = context.ReadValueAsButton();
    }
}