using UnityEngine; 
using System.Collections; 

public abstract class ItemClass
{
    [Header("Item")]
    //Public to access everywhere 
    public string ItemName; 
    public Sprite itemIcon; 

    public abstract ItemClass GetItem(); 
    public abstract JewelleryClass GetJewellery(); 
    public abstract MiscClass GetMisc(); 
    public abstract ConsumableClass GetConsumable(); 
}