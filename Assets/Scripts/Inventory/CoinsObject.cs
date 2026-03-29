using UnityEngine; 

[CreateAssetMenu(fileName = "New Coins Object", menuName = "Inventory System/Items/Coins")]
public class CoinsObject : ItemObject
{
    public int coinsAmount; 
    
    public void Awake()
    {
        type = ItemType.Coins; 
    }
}