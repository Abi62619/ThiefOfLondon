using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public PlayerInventory inventory;

    [Header("Input")]
    public InputActionAsset inputActionAsset;

    private InputAction saveAction;
    private InputAction loadAction;

    void OnEnable()
    {
        var actionMap = inputActionAsset.FindActionMap("Player");

        saveAction = actionMap.FindAction("Save");
        loadAction = actionMap.FindAction("Load");

        saveAction.performed += OnSave;
        loadAction.performed += OnLoad;

        saveAction.Enable();
        loadAction.Enable();
    }

    void OnDisable()
    {
        saveAction.performed -= OnSave;
        loadAction.performed -= OnLoad;

        saveAction.Disable();
        loadAction.Disable();
    }

    void OnSave(InputAction.CallbackContext ctx)
    {
        InventorySaveSystem.SaveInventory(inventory);
    }

    void OnLoad(InputAction.CallbackContext ctx)
    {
        InventorySaveSystem.LoadInventory(inventory);
    }
}
