using UnityEngine; 
using System.Collections.Generic; 

public class TradingManager : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public TraderInventory traderInventory;  
    public static TradingManager Instance; 
    public List<Item> traderItems = new List<Item>();
    [HideInInspector] private Item selectedItem; 
    [HideInInspector] private string sourceInventory; 
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void SelectItem(int slotIndex, string inventoryName)
    {
        if (inventoryName == "PlayerInventoryPanel")
        {
            if (slotIndex >= playerInventory.items.Count) return;

            selectedItem = playerInventory.items[slotIndex];
            sourceInventory = "PlayerInventoryPanel";

            SafeTransferItem(selectedItem, playerInventory.items, traderItems);
        }
        else if (inventoryName == "TraderInventoryPanel")
        {
            if (slotIndex >= traderItems.Count) return;

            selectedItem = traderItems[slotIndex];
            sourceInventory = "TraderInventoryPanel";

            SafeTransferItem(selectedItem, traderItems, playerInventory.items);
        }

        selectedItem = null;
    }

    private void UpdateUI()
    {
        // Implementation to update UI elements based on current inventory state.
        // Iterate through PlayerInventoryPanel and TraderInventoryPanel,
        // and set the Image component of each ItemSlot to the corresponding item's icon,
        // or disable the image if the slot is empty.
    }
    
    public bool CanTransferItem(Item item, IList<Item> sourceInventory, IList<Item> destinationInventory)
    {
        if(item == null)
        {
            Debug.LogError("Invalid item.");
            return false;
        }

        if (!sourceInventory.Contains(item))
        {
            Debug.LogError("Source inventory does not contain item.");
            return false;
        }

        return true;
    }

    public void SafeTransferItem(Item item, IList<Item> sourceInventory, IList<Item> destinationInventory)
    {
        if(CanTransferItem(item, sourceInventory, destinationInventory))
        {
            sourceInventory.Remove(item);
            destinationInventory.Add(item);

            UpdateUI();
        }
        else
        {
            Debug.LogError("Item transfer failed due to validation errors.");
        }
    }
}