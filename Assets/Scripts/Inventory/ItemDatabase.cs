using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Database", menuName = "Inventory System/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<Item> items = new List<Item>();
}