using UnityEngine;

[CreateAssetMenu(fileName = "New Drink Object", menuName = "Inventory System/Items/Drink")]
public class DrinkObject : ItemObject
{
    public int restoreThristValue; 
    

    public void Awake()
    {
        type = ItemType.Drink; 
    }
}