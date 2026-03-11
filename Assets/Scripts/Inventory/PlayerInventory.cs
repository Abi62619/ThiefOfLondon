using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    [Header("Input")]
    public InputActionAsset inputActionAsset;

    private InputAction saveAction;
    private InputAction loadAction;

    [Header("Items")]
    public List<Item> items = new List<Item>();  //list of players items 
    public ItemDatabase database;  // database of items player can get  

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
        if (saveAction != null)
        {
            saveAction.performed -= OnSave;
            saveAction.Disable();
        }

        if (loadAction != null)
        {
            loadAction.performed -= OnLoad;
            loadAction.Disable();
        }
    }

    void OnSave(InputAction.CallbackContext context)
    {
        Debug.Log("Save triggered");

        if (!context.performed) return;

        InventorySaveSystem.SaveInventory(this);
    }

    void OnLoad(InputAction.CallbackContext context)
    {
        Debug.Log("Load triggered");

        if (!context.performed) return;

        InventorySaveSystem.LoadInventory(this);
    }

    public InventoryData GetSaveData()
    {
        InventoryData data = new InventoryData();

        data.itemId = new List<int>();

        foreach (Item item in items) // if the items added make new in the list 
        {
            if (item != null)
                data.itemId.Add(item.itemId);
        }

        return data;
    }

    public void LoadData(InventoryData data)
    {
        if (database == null)
        {
            Debug.LogError("ItemDatabase is NULL!");
            return;
        }

        items.Clear();

        foreach (int id in data.itemId)
        {
            Item item = database.GetItem(id);  // get the item id from the database

            if (item != null)
            {
                items.Add(item);  // add the item 
            }
            else
            {
                Debug.LogWarning("Item ID not found in database: " + id);
            }
        }

        Debug.Log("Inventory loaded. Item count: " + items.Count);
    }
}