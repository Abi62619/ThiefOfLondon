using UnityEngine; 
using System.Collections; 

public abstract class ItemClass : ScriptableObject // Makes all ItemClass scripts become ScriptableObjects
{
    [Header("Item")] //Data shared across items 
    //Public to access everywhere 
    public string ItemName; 
    public Sprite itemIcon; 

    public abstract ItemClass GetItem(); 
    public abstract JewelleryClass GetJewellery(); 
    public abstract MiscClass GetMisc(); 
    public abstract ConsumableClass GetConsumable(); 
}