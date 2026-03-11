using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    //need to test the method see if it happpens with other methods 
    //need to test if it is the hold in the input system

    [Header("Input")]
    public InputActionAsset inputActionAsset;

    private InputAction saveAction;
    private InputAction loadAction;

    [Header("Items")]
    public List<Item> items = new List<Item>();  //list of players items 
    public ItemDatabase database;  // database of items player can get  
    public int objects; //players total objects 

    void OnEnable()
    {
        var actionMap = inputActionAsset.FindActionMap("Player");

        saveAction = actionMap.FindAction("Save");
        loadAction = actionMap.FindAction("Load");

        saveAction.performed += OnSave;
        loadAction.performed += OnLoaded;

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
            loadAction.performed -= OnLoaded;
            loadAction.Disable();
        }
    }

    void OnSave(InputAction.CallbackContext context)
    {
        Debug.Log("Save triggered");

        if (!context.performed) return;

        InventorySaveSystem.SaveInventory(this);
    }

    void OnLoaded(InputAction.CallbackContext context)
    {
        Debug.Log("Load triggered");

        if (!context.performed) return;

        InventorySaveSystem.LoadInventory(this);
    }

    /*void OnTest(InputAction.CallbackContext context)
    {
        Debug.Log("Test Load Trigger"); 
    }*/

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

    public void AddObjects(int amount)
    {
        objects += amount;

        Item objectItem = database.GetItem(0); // item id

        for (int i = 0; i < amount; i++)
        {
            items.Add(objectItem);
        }

        Debug.Log("Objects gained: " + amount);
    }

    public void AddItem(Item item)
    {
        if (item == null) return;

        items.Add(item);

        Debug.Log("Item added: " + item.name);
    }
}