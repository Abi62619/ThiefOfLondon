using UnityEngine;

[CreateAssetMenu(fileName = "New Drink Object", menuName = "Inventory System/Items/Drink")]
public class DrinkObject : ItemObject
{
    public int thristRestoreValue; 
    
    public void Awake()
    {
        type = ItemType.Drink; 
    }
}
