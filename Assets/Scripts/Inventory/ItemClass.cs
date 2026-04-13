using System.Collections; 
using UnityEngine; 

public abstract class ItemClass
{
    [Header("Item Info")]
    public string itemName;
    public Sprite itemIcon;

    public abstract ItemClass GetItem();
    public abstract ItemClass GetTool(); 
    public abstract MiscClass GetMisc();
    public abstract ConsumableClass GetConsumable();
}