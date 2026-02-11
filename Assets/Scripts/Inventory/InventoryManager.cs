using UnityEngine; 
using System.Collections; 
using System.Collections.Generic; 

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject slotHolder;
    [SerializeField] private ItemClass itemToAdd; 
    [SerializeField] private ItemClass itemToRemove; 

    // List in the inspector 
    public List <ItemClass> items = new List<ItemClass>(); 
    private GameObject[] slots; 

    public void Start()
    {
        slots = new GameObject[slotHolder.transform.childCount];
        //set all the slots
        for(int i = 0; i < slotHolder.transform.childCount; i++)
            slots[i] = slotHolder.transform.GetChild(i).gameObject;

        Add(itemToAdd); 
        Remove(itemToRemove); 
    }

    public void Add(ItemClass item)
    {
        items.Add(item); 
        Debug.Log("Added item to player inventory");
    }

    public void Remove(ItemClass item)
    {
        items.Remove(item); 
        Debug.Log("Removed item from player inventory");
    }
}