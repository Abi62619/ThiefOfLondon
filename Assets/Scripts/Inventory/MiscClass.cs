using UnityEngine;

[CreateAssetMenu(fileName = "New Misc Class", menuName = "Inventory/Misc")]
public class MiscClass : ItemClass
{
    [Header("Misc")]
    public MiscType miscType;

    public enum MiscType 
    { 
        Ore, 
        Gem, 
        Material, 
        Coin, 
        Other 
    }

    public abstract ItemClass GetItem() { return this; }
    public abstract ToolClass GetTool() { return null; }
    public abstract MiscClass GetMisc() { return this; }
    public abstract ConsumableClass GetConsumable() { return null; }    
}