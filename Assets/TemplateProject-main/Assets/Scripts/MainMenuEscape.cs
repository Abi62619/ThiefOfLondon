using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuEscape : MonoBehaviour
{
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private GameObject PlayerPanel;
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private GameObject QuitPanel;

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

     void OnEscape(InputAction.CallbackContext context)
    {
        if (!MainMenuPanel.activeSelf)
        {
            SettingsPanel.SetActive(false);
            PlayerPanel.SetActive(false);
            MainMenuPanel.SetActive(true);
        }
        else if (!SettingsPanel.activeSelf && !PlayerPanel.activeSelf && MainMenuPanel.activeSelf && !QuitPanel.activeSelf)
        {
            QuitPanel.SetActive(true);

        }else if (QuitPanel.activeSelf)
        {
            QuitPanel.SetActive(false);

        }

    }

}

