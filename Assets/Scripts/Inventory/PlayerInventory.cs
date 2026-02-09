using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int coins;

    public List<ItemObject> items = new List<ItemObject>();

    public void AddCoin(int amount)
    {
        coins += amount;
        Debug.Log("Coins: " + coins);
    }

    public void AddItem(ItemObject item)
    {
        items.Add(item);
        Debug.Log("Added item: " + item.name);
    }
}
