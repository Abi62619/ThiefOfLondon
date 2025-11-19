using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerNew : MonoBehaviour
{
    // Reference to your Input Action Asset
    public InputActionAsset inputActionAsset;

    // Input Actions // Action to bind and use
    private InputAction moveAction;
    private InputAction interactAction;
    private InputAction jumpAction;

    // Movement variables
    private Vector2 moveInput;
    public float moveSpeed = 5f;

    private void OnEnable()
    {
        // Find the specific action map and actions
        var actionMap = inputActionAsset.FindActionMap("Player");  // Replace "Player" with the name of your action map
        moveAction = actionMap.FindAction("Move");                // Replace "Move" with the name of your move action
        interactAction = actionMap.FindAction("Interact");        // Replace "Interact" with the name of your interact action
        jumpAction = actionMap.FindAction("Jump");

        // Enable actions
        moveAction.Enable();
        interactAction.Enable();
        jumpAction.Enable();


        // Subscribe to input events
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;  // Handle when movement input stops
        interactAction.performed += OnInteract;
        jumpAction.performed += OnJump;
       
    }

    private void OnDisable()
    {
        // Unsubscribe from input events
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
        interactAction.performed -= OnInteract;
        jumpAction.performed -= OnJump;
        

        // Disable actions
        moveAction.Disable();
        interactAction.Disable();
        jumpAction.Disable();
    }

    // Move input handler
    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();  // Read the 2D move vector from the input
    }

    // Interact button handler
    private void OnInteract(InputAction.CallbackContext context)
    {
        // Perform interaction logic here
        Debug.Log("Interact button pressed!");
    }

    private void OnJump (InputAction.CallbackContext context)
    {
        Debug.Log("You Jumped");
    }

    private void Update()
    {        
        /*if (jumpAction.ReadValue<float>()!=0) // Use this if you want continious input
        {
            Debug.Log("Jump Continoues");
        }*/

    }
}
