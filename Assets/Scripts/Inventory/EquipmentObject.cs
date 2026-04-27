using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObject : ItemObject
{
    public int coinValue; 
    //Need to link to surivial manager 
    public void Awake()
    {
        type = ItemType.Equipment; 
    }
}