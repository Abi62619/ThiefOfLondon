using UnityEngine; 

[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Default")]

public class FoodObject : ItemObject 
{
    public int restoreHealthValue; 

    public void Awake()
    {
        type = ItemType.Food; 
    }
}