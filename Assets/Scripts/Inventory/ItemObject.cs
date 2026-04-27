using UnityEngine;

public enum ItemType //Add the others later 
{
    Food, 
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
