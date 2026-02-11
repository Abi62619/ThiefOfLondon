using UnityEngine; 
using System.Collections; 

[CreateAssetMenu(fileName = "New Misc Class", menuName =  "Item/Misc")]
public class MiscClass : ItemClass
{
    [Header("Miscellaneous")] //Data for misc s

    //What Object it is returning 
    public override ItemClass GetItem() {return this;}
    public override JewelleryClass GetJewellery() {return null;}
    public override MiscClass GetMisc() {return this;}
    public override ConsumableClass GetConsumable() {return null;}
}