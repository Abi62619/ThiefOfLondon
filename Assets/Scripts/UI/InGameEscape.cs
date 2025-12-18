using UnityEngine;
using UnityEngine.InputSystem;

public class InGameEscape : MonoBehaviour
{
    public GameObject settingsMenu;

    // Reference to your Input Action Asset
    public InputActionAsset inputActionAsset;

    // Input Actions // Action to bind and use
    private InputAction escapeAction;

    void OnEnable()
    {
        // Find the specific action map and actions
        var actionMap = inputActionAsset.FindActionMap("Player");
        escapeAction = actionMap.FindAction("Escape");

        // Enable actions
        escapeAction.Enable();
        // Subscribe to input events
        escapeAction.performed += OnEscape;
    }

    void OnDisable()
    {
        // Unsubscribe from input events
        escapeAction.performed -= OnEscape;
        // Disable actions
        escapeAction.Disable();
    }

  public   void OnEscape(InputAction.CallbackContext context)
    {
        
        if (!settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            settingsMenu.SetActive(false);
            Time.timeScale = 1f;
        }


    }

}
