using UnityEngine;

public class DisplayInventoty : MonoBehaviour
{
    public InventoryObject inventory; 

    public int  X_SPACE_BETWEEN_ITEM; 
    public int NUMBER_OF_COLUMN; 
    public int Y_SPACE_BETWEEN_ITEM; 

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    void Start()
    {
        CreateDisplay();
    } 

    void Update()
    {
        UpdateDisplay(); 
    }

    void CreateDisplay()
    {

    }
    
    void UpdateDisplay()
    {
    
    }
}
