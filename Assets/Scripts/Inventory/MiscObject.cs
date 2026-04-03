using UnityEngine;

[CreateAssetMenu(fileName = "New Misc Object", menuName = "Inventory System/Items/Misc")]
public class MiscObject : ItemObject
{
    public int coinValue; 

    public void Awake()
    {
        type = ItemType.Misc; 
    }
}
