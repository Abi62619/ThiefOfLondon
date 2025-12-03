using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickPocketManger : MonoBehaviour
{
    public InputActionAsset inputActionAsset;

    private InputAction interactAction;

    public LayerMask NPCmask; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        NPCmask = LayerMask.GetMask("NPC", "Player"); 
    }

    private void OnEnable()
    {
        var actionMap = inputActionAsset.FindActionMap("Player");
        interactAction = actionMap.FindAction("Interact");

        interactAction.Enable();

        interactAction.performed += OnInteract;
    }

    private void OnDisable()
    {
        interactAction.performed -= OnInteract;

        interactAction.Disable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interact button pressed!");
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, NPCmask))

        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            Debug.Log("Did Hit");
        }
    }
}
