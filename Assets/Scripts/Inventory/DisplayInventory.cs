using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem; 

public class DisplayInventory : MonoBehaviour
{
    [Header("Input System")]
    public InputActionAsset inputActionAsset;
    public GameObject inventoryUI;
    private InputAction showAction;

    [Header("Display Inventory")]
    public InventoryObject inventory;
    public int X_START; 
    public int Y_START; 
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;

    Dictionary<int, GameObject> itemsDisplayed = new Dictionary<int, GameObject>();

    void Start()
    {
        //CreateDisplay();
    }

    void Update()
    {
        if (inventoryUI.activeSelf)
            UpdateDisplay();
    }

    void OnEnable()
    {
        var actionMap = inputActionAsset.FindActionMap("Player");

        showAction = actionMap.FindAction("Show");

        actionMap.Enable();
        showAction.Enable(); 

        showAction.performed += OnShowing;
    }

    void OnDisable()
    {
        showAction.Disable();
        showAction.performed -= OnShowing; 
    }     

    public void OnShowing(InputAction.CallbackContext context)
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);

        if (inventoryUI.activeSelf)
            CreateDisplay();
        else
            ClearDisplay();
    } 

    public void CreateDisplay()
    {
        for(int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponent<Image>().sprite = inventory.Container[i].item.sprite;
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            itemsDisplayed.Add(i, obj);
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i/NUMBER_OF_COLUMN)), 0f);
    }

    public void UpdateDisplay()
    {
        for(int i = 0; i < inventory.Container.Count; i++)
        {
            if(itemsDisplayed.ContainsKey(i))
            {
                itemsDisplayed[i].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0"); 
            }
            else
            {
                var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponent<Image>().sprite = inventory.Container[i].item.sprite;
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                itemsDisplayed.Add(i, obj);
            }
        }
    }

    void ClearDisplay()
    {
        foreach (var item in itemsDisplayed.Values)
            Destroy(item);
        itemsDisplayed.Clear();
    }
}