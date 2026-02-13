using UnityEngine; 
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine.UI; 

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

        RefreshUI(); 
        Add(itemToAdd); 
        Remove(itemToRemove); 
    }

    public void RefreshUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].itemIcon; 
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true; 
            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false; 
            }
        }
    }

    public void Add(ItemClass item)
    {
        items.Add(item); 
        Debug.Log("Added item to player inventory");
        RefreshUI();
    }

    public void Remove(ItemClass item)
    {
        items.Remove(item); 
        Debug.Log("Removed item from player inventory");
        RefreshUI();
    }
}