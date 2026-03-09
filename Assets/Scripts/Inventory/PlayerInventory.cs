using UnityEngine; 
using System.Collections.Generic; 

public class PlayerInventory : MonoBehaviour
{
    public List<Item> items = new List<Item>(); 

    public InventoryData GetSaveData()
    {
        InventoryData data = new InventoryData();; 
        data.items = items;
        return data; 
    }

    public void LoadData(InventoryData data)
    {
        items = data.items; 
    }
}