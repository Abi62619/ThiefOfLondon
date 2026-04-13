using System.Collections;
using UnityEngine;
using System.collections.Generic;

public class ConsumableClass : ItemClass
{
    [Header("Consumable Info")]
    public float effectValue;

    public enum ConsumableType
    {
        food,
        drink
    }

    public override ItemClass GetItem() {return this;}
    public override ToolClass GetTool() {return this;}
    public override MiscClass GetMisc() {return null;}
    public override ConsumableClass GetConsumable() {return null;}
}