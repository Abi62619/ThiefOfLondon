using UnityEngine; 
using System.Collections; 

[CreateAssetMenu(fileName = "new Jewellery Class", menuName= "Item/Jewellery")]
public class JewelleryClass : ItemClass
{
    [Header("Jewellery")] //Data for jewellery 
    public JewelleryType jewelleryType; 

    public enum jewelleryType
    {
        necklace, 
        ring,
        bracelet, 
        watch 
    }

    //What Object it is returning 
    public override ItemClass GetItem() {return this;}
    public override JewelleryClass GetJewellery() {return this;}
    public override MiscClass GetMisc() {return null;}
    public override ConsumableClass GetConsumable() {return null;}
}