using UnityEngine;

public enum ItemType
{
    Food, 
    Drink,
    Coins,
    Jewelry, 
    Equipment, 
    Default
}

public class ItemObject : ScriptableObject
{
    public GameObject prefab; 
    public ItemType type; 
    [TextArea(15, 20)]
    public string description; 
}
