using UnityEngine; 
using System.Collections; 

[CreateAssetMenu(fileName = "new Consumable Class", menuName = "Item/Consumable")]
public class ConsumableClass : ItemClass
{
    [Header("Consumables")] //Data for consumable  
    public float healthAdded; 

    //What Object it is returning 
    public override ItemClass GetItem() {return this;}
    public override JewelleryClass GetJewellery() {return null;}
    public override MiscClass GetMisc() {return null;}
    public override ConsumableClass GetConsumable() {return this;}
}