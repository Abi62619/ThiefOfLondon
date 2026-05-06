using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System/Items/Food")]
public class FoodObject : ItemObject
{
    public int restoreCurrentHunger; 
    //Need to link to surivial manager 
    public void Awake()
    {
        type = ItemType.Food; 
    }
}