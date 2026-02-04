using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int coin; 

    public void AddCoin(int amount)
    {
        coin += amount;
        Debug.Log("Coins gained: " + amount + ". Total coins: " + coin);
    }
}
