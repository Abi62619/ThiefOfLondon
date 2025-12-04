using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickPocketManger : MonoBehaviour
{
    public InputActionAsset inputActionAsset;

    private InputAction interactAction;
    private PickpocketDetector detector;

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
        detector = GetComponent<PickpocketDetector>();
    }

    private void OnEnable()
    {
        Debug.Log("PickPocketManager ENABLED");

        var actionMap = inputActionAsset.FindActionMap("Player");
        interactAction = actionMap.FindAction("Pickpocket");

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

        if (detector.currentTarget != null)
        {
            Debug.Log($"Pickpocketed: {detector.currentTarget.name}");
        }
        else
        {
            Debug.Log("No target in range to pickpocket.");
        }
    }
}