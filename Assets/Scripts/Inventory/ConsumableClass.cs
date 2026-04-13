using UnityEngine;

public class ConsumableClass : ItemClass
{
    [Header("Consumable")]
    public ConsumableType consumableType;
    public float hungerRestore;
    public float thirstRestore;
    public float staminaRestore;

    public enum ConsumableType 
    { 
        Food, 
        Drink, 
        Medicine 
    }

    public abstract ItemClass GetItem() { return this; }
    public abstract ToolClass GetTool() { return null; }
    public abstract MiscClass GetMisc() { return null; }
    public abstract ConsumableClass GetConsumable() { return this; }    
}