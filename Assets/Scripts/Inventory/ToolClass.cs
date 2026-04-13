using UnityEngine;

[CreateAssetMenu(fileName = "New Tool Class", menuName = "Inventory/Tool")]
public class ToolClass : ItemClass
{
    [Header("Tool")]
    public ToolType toolType;

    public enum ToolType 
    { 
        Pickaxe, 
        Axe, 
        Shovel, 
        Sword, 
        Shield
    }

    public abstract ItemClass GetItem() { return this; }
    public abstract ToolClass GetTool() { return this; }
    public abstract MiscClass GetMisc() { return null; }
    public abstract ConsumableClass GetConsumable() { return null; }    
}