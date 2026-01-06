using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int gold; 

    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log("Gold gained: " + amount + ". Total gold: " + gold);
    }
}
