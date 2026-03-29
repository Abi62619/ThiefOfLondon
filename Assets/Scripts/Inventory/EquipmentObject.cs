using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObject : ItemObject
{
    public float pickpocketBonus; 
    public float hidingBonus; 
    
    public void Awake()
    {
        type = ItemType.Equipment; 
    }
}