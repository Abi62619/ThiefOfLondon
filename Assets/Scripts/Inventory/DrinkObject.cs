using UnityEngine;

[CreateAssetMenu(fileName = "New Drink Object", menuName = "Inventory System/Items/Drink")]
public class DrinkObject : ItemObject
{
    public int restoreThristBar; 

    public void Awake()
    {
        type = ItemType.Drink; 
    }
}