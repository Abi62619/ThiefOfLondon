using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;

    [Header("Inventory Input Setup")]
    public InputActionAsset inputActionAsset;

    private InputAction saveAction;
    private InputAction loadAction; 

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>(); 
        if(item)
        {
            inventory.AddItem(item.item, 1); 
            Destroy(other.gameObject); 
        }
    }

    void OnEnable()
    {
        var actionMap = inputActionAsset.FindActionMap("Player");

        saveAction = actionMap.FindAction("Save");
        loadAction = actionMap.FindAction("Load");

        actionMap.Enable();

        saveAction.Enable(); 
        loadAction.Enable(); 

        saveAction.performed += OnSave;

        loadAction.performed += OnLoad;
    }

    void OnDisable()
    {
        saveAction.Disable();
        loadAction.Disable();

        saveAction.performed -= OnSave; 
        loadAction.performed -= OnLoad; 
    }

    public void OnSave(InputAction.CallbackContext context)
    {
        inventory.Save(); 
        Debug.Log("Saving Inventory"); 
    }

    public void OnLoad(InputAction.CallbackContext context)
    {
        inventory.Load(); 
        Debug.Log("Loading Inventory"); 
    }

        private void OnApplicationQuit()
    {
        inventory.Container.Clear(); 
    }
}