using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public ItemClass itemToAdd; 
    public ItemClass itemToRemove;

    public List<ItemClass> items = new List<ItemClass>();

    public void Start()
    {
        if (itemToAdd != null)
        {
            AddItem(itemToAdd);
        }

        if (itemToRemove != null)
        {
            RemoveItem(itemToRemove);
        }
    }

    public void AddItem(ItemClass item)
    {
        items.Add(item);
        Debug.Log("Added item: " + item.itemName);
    }

    public void RemoveItem(ItemClass item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log("Removed item: " + item.itemName);
        }
        else
        {
            Debug.Log("Item not found in inventory: " + item.itemName);
        }
    }
}