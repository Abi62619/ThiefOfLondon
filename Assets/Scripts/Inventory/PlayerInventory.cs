using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI; 

public class PlayerInventory : MonoBehaviour
{
    //Finish watching https://www.youtube.com/watch?v=AoD_F1fSFFg

    [Header("Input")]
    public InputActionAsset inputActionAsset;

    private InputAction saveAction;
    private InputAction loadAction;

    [Header("Items")]
    public List<Item> items = new List<Item>();  //list of players items 
    public ItemDatabase playerDatabase;  // database of items player can get  
    public int objects; //players total objects 

    [Header("Inventory UI")]
    public static PlayerInventory Instance; //singleton
    public Transform itemContent; //UI container 
    public GameObject inventoryItem; //prefab 

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

    void Awake()
    {
        Instance = this; 
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

        foreach (Item item in items)
        {
            if (item != null)
                data.itemId.Add(item.itemId);
        }

        data.objects = objects;

        return data;
    }

    public void LoadData(InventoryData data)
    {
        if (playerDatabase == null)
        {
            Debug.LogError("ItemDatabase is NULL!");
            return;
        }

        items.Clear();

        foreach (int id in data.itemId)
        {
            Item item = playerDatabase.GetItem(id);

            if (item != null)
            {
                items.Add(item);
            }
            else
            {
                Debug.LogWarning("Item ID not found in database: " + id);
            }
        }

        objects = data.objects;

        Debug.Log("Inventory loaded. Item count: " + items.Count);
    }

    public void AddObjects(int amount)
    {
        Debug.Log("=== ADD OBJECTS CALLED ===");

        for (int i = 0; i < amount; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, playerDatabase.items.Count);

            Debug.Log("Random index: " + randomIndex);

            Item objectItem = playerDatabase.items[randomIndex];

            if (objectItem == null)
            {
                Debug.LogError("Item is NULL!");
                continue;
            }

            Debug.Log("Adding item: " + objectItem.name);

            items.Add(objectItem);
        }

        Debug.Log("FINAL COUNT: " + items.Count);
    }

    public void AddItem(Item item)
    {
        if (item == null) return;

        items.Add(item);

        Debug.Log("Item added: " + item.name);
    }

    void OnApplicationQuit()
    {
        InventorySaveSystem.SaveInventory(this);
    }

    /*public void ListItems()
    {
        foreach (var item in items)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);

            var itemName = obj.transform.Find("Item/ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("Item/ItemIcon").GetComponent<Image>();

            itemName.text = item.name;
            itemIcon.sprite = item.icon; 
        }
    }*/
}