using UnityEngine; 
using System.Collections; 

public class InventoryManager : MonoBehaviour
{
    public ItemClass itemToAdd; 
    public ItemClass itemToRemove; 

    // List in the inspector 
    public List<ItemClass> items = new List<ItemClass>(); 

    public void Start()
    {
        Add(itemToAdd); 
        Remove(itemToRemove); 
    }

    public void Add(ItemClass item)
    {
        items.Add(item); 
        Debug("Added item to player inventory");
    }

    public void Remove(ItemClass item)
    {
        items.Remove(item); 
        Debug("Removed item from player inventory");
    }
}