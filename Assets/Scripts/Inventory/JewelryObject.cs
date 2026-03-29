using UnityEngine;

[CreateAssetMenu(fileName = "New Jewelry Object", menuName = "Inventory System/Items/Jewelry")]
public class JewelryObject : ItemObject
{
    public int jewelryAmount; 
    
    public void Awake()
    {
        type = ItemType.Jewelry; 
    }
}