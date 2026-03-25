using UnityEngine;
using System.Collections.Generic;

public class TraderInventory : MonoBehaviour
{
    [Header("Trader Items")]
    public List<Item> items = new List<Item>(); 
    public ItemDatabase traderDatabase;  
    public int traderObjects = 5; // how many items trader has

    void Start()
    {
        GenerateTraderInventory();
    }

    public void GenerateTraderInventory()
    {
        items.Clear();

        for (int i = 0; i < traderObjects; i++)
        {
            int randomIndex = Random.Range(0, traderDatabase.items.Count);
            Item randomItem = traderDatabase.items[randomIndex];

            items.Add(randomItem);
        }
    }
}