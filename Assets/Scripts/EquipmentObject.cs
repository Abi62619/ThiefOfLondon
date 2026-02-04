using UnityEngine; 

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]

public class EquipmentItem : ItemObject 
{
    public GameObject ClothingItem; 

    public void Awake ()
    {
        type = ItemType.Equipment; 
    }
}