using UnityEngine;
using UnityEngine.InputSystem; 

[RequireComponent(typeof(PlayerControlTryf))]
public class PlayerCrouch : MonoBehaviour
{
    [Header("Crouch Settings")]
    [SerializeField] private float crouchHeight = 1f; 
    [SerializeField] private float standingHeight = 2f; 
    private CapsuleCollider capsuleCollider; 
    private bool isCrouching; 

    [Header("Input Actions")]
    public InputActionAsset inputActionAsset; 
    private InputAction crouchAction; 

    private void OnEnable()
    {
        //Debug.Log("Crouch can happen"); 

        if (inputActionAsset == null)
        {
            //Debug.LogError("InputActionAsset NOT assigned!");
            return;
        }

        var actionMap = inputActionAsset.FindActionMap("Player");
        if (actionMap == null) return;

        crouchAction = actionMap.FindAction("Crouch");
        if (crouchAction == null) return;

        crouchAction.Enable();
        crouchAction.performed += OnCrouch;
        crouchAction.canceled += OnCrouch;

        //Debug.Log(crouchAction != null ? "Crouch action found" : "Crouch action MISSING");
        //Debug.Log($"Action map found: {actionMap.name}");
    }

    private void OnDisable()
    {
        crouchAction.performed -= OnCrouch; 
        crouchAction.canceled -= OnCrouch; 

        crouchAction.Disable(); 
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        //Debug.Log($"Crouch phase: {context.phase}");
        isCrouching = context.ReadValueAsButton();
    }

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>(); 
        standingHeight = capsuleCollider.height;

        /*if (capsuleCollider == null)
        {
            Debug.LogError("CapsuleCollider missing!");
        }*/
    }

    void Update()
    {
        capsuleCollider.height = isCrouching ? crouchHeight : standingHeight;
    }
}
