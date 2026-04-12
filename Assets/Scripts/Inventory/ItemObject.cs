using UnityEngine;

public enum ItemType
{
    Food, 
    Equipment, 
    Weapon, 
    Drink, 
    Jewelery, 
    Coin, 
    Default
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab; 
    public ItemType type; 
    [TextArea(15, 20)]
    public string description; 
}
