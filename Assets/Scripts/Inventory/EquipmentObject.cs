using UnityEngine; 

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]

public class EquipmentItem : ItemObject 
{
    public float attackBonus;
    public float defenseBonus;

    public void Awake ()
    {
        type = ItemType.Equipment; 
    }
}