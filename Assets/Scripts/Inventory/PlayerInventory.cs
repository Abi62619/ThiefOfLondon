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
    public List<Item> items = new List<Item>();
    public ItemDatabase database;

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
        InventorySaveSystem.SaveInventory(this);
    }

    void OnLoad(InputAction.CallbackContext ctx)
    {
        InventorySaveSystem.LoadInventory(this);
    }

    public InventoryData GetSaveData()
    {
        InventoryData data = new InventoryData();

        foreach (var item in items)
        {
            data.itemIds.Add(item.itemId);
        }

        return data;
    }

    public void LoadData(InventoryData data)
    {
        items.Clear();

        foreach (int id in data.itemIds)
        {
            Item item = database.GetItem(id);

            if (item != null)
                items.Add(item);
        }
    }
}