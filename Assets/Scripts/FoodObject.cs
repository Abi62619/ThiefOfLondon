using UnityEngine; 

[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System/Items/Food")]

public class FoodObject : ItemObject 
{
    public int restoreHealthValue; 

    public void Awake()
    {
        type = Itemtype.Food; 
    }
}