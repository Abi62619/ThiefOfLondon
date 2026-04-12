using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObject : ItemObject
{
    public int hidingBonus; 
    public int coinValue; 

    public void Awake()
    {
        type = ItemType.Equipment; 
    }
}