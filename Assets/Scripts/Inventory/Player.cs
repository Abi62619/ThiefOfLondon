using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory; 
    
    public void AddItem(ItemObject item)
    {
        inventory.AddItem(item, 1);
        Debug.Log($"[Player] Added {item.name} to inventory");
    }
}