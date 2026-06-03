using UnityEngine;

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab; 
    public Sprite sprite; 
    public ItemType type; 
    [TextArea(15,20)]
    public string description; 
}
