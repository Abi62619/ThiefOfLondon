using UnityEngine;
using System.Collections.Generic; 

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> inventory; 
    public Dictionary<ItemData, InventoryItem> itemDictionary; 
}