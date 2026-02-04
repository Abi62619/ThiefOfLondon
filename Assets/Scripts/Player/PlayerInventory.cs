using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //Debug test for coins, will be removed later
    [SerializeField] private int coin; 

    public void AddCoin(int amount)
    {
        coin += amount;
        Debug.Log("Coins gained: " + amount + ". Total coins: " + coin);
    }

    public InventoryObject inventory;
    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<ItemObject>();
        if (item)
        {
            inventory.AddItem(item, 1);
            Debug.Log("Picked up: " + item.name);
            Destroy(other.gameObject);
        }
    }
}
