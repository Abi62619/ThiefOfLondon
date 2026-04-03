using UnityEngine;

[CreateAssetMenu(fileName = "New Coin Object", menuName = "Inventory System/Items/Coin")]
public class CoinObject : ItemObject
{
    public int coinValue; 

    public void Awake()
    {
        type = ItemType.Coin; 
    }
}