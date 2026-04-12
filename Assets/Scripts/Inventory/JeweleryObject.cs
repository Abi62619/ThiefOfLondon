using UnityEngine;

[CreateAssetMenu(fileName = "New Jewelery Object", menuName = "Inventory System/Items/Jewelery")]
public class JeweleryObject : ItemObject
{
    public int coinValue; 

    public void Awake()
    {
        type = ItemType.Jewelery; 
    }
}