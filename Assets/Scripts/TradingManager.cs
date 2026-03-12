using UnityEngine; 
using System.Collections.Generic; 

public class TradingManager : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public TraderInventory traderInventory;  
    [SerializeField] public static TradingManager Instance; 
    [SerializeField] public List<Item> playerInventory = new List<Item>();
    [SerializeField] public List<Item> traderInventory = new List<Item>();
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
            selectedItem = playerInventory[slotIndex];
            sourceInventory = "PlayerInventoryPanel";
        }
        else if (inventoryName == "TraderInventoryPanel")
        {
            selectedItem = traderInventory[slotIndex];
            sourceInventory = "TraderInventoryPanel";
        }

        UpdateUI(); //refresh UI after transfer 
        selectedItem = null; //reset selection 
        
    }

    private void UpdateUI()
    {
        // Implementation to update UI elements based on current inventory state.
        // Iterate through PlayerInventoryPanel and TraderInventoryPanel,
        // and set the Image component of each ItemSlot to the corresponding item's icon,
        // or disable the image if the slot is empty.
    }
    
    public bool CanTransferItem(Item item, List<Item> sourceInventory, List<Item> destinationInventory)
    {
        //check if item is valid
        if(item == null)
        {
            Debug.LogError("Invalid item.");
            return false; 
        }

        //check if source inventory contains the item
        if (!sourceInventory.Contains(item))
        {
            Debug.LogError("Source inventory does not contain item.");
            return false; 
        }

        return true; 
    }

    public void SafeTransferItem(Item item, List<ItemSlot> sourceInventory, List<Item> destinationInventory)
    {
        if(CanTransferItem(item,sourceInventory, destinationInventory))
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