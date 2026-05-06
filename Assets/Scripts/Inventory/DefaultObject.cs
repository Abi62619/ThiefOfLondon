using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/default")]
public class DefaultObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Default; 
    }
}
