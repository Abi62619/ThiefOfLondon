using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable] 
public class Item
{
    public ItemType itemType; 
    public int itemId; 
    public string name; 
    public Sprite icon; 

    public enum ItemType
    {
        Coins, 
        Necklace, 
        Bracelet, 
        Handkerchiefs, 
        Purses 
    }
}

[Serializable]
public class InventoryData
{
    public List<Item> items = new List<Item>(); 
}