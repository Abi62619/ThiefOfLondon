using UnityEngine; 

[CreateAssetMenu(fileName = "New Coins Object", menuName = "Inventory System/Items/Coin")]

public class CoinItem : ItemObject 
{
    public int Coins; 

    public void Awake ()
    {
        type = ItemType.Coin; 
    }
}