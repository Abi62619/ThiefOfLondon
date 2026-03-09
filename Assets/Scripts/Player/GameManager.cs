using UnityEngine; 

public class GameManager : MonoBehaviour
{
    public PlayerInventory inventory; 

    [Header("Input")]
    public InputActionAsset inputActionAsset;
    private InputAction saveAction; 
    private InputAction loadAction; 

    #region Input Setup 

    void OnEnable()
    {
        var actionMap = inputActionAsset.FindActionMap("Player");

        saveAction = actionMap.FindAction("Save");
        loadAction = actionMap.FindAction("Load");

        actionMap.Enable();

        saveAction.Enable(); 
        loadAction.Enable();
    }

    void OnDisable()
    {
        saveAction.Disable();
        loadAction.Disable();
    }

    #endregion
    #region Input commands 

    void Awake()
    {
        input = new PlayerInputActions();

        input.Player.SaveInventory.performed += ctx =>
            InventorySaveSystem.SaveInventory(inventory);

        input.Player.LoadInventory.performed += ctx =>
            InventorySaveSystem.LoadInventory(inventory);
    }

    #endregion
}