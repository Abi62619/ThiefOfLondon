using UnityEngine; 
using System.Collections.Generic; 
using System;

[System.Serializable] 
public abstract class Item : ScriptableObject
{
    public ItemType itemType; 
    public int itemId; 
    public string itemName; 
    public Sprite icon; 
}

public enum ItemType
{
    Coins, 
    Necklace, 
    Bracelet, 
    Handkerchiefs, 
    Purses 
}

[Serializable]
public class InventoryData
{
    public List<int> itemId = new List<int>();
    public int objects; 
}