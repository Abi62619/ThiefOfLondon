using UnityEngine;

public class ToolClass : ItemClass
{
    [Header("Tool Info")]
    public ToolType toolType;

    public enum ToolType
    {
        sword,
        shield,
        helmet
    }

    public override ItemClass GetItem() {return this;}
    public override ToolClass GetTool() {return this;}
    public override MiscClass GetMisc() {return null;}
    public override ConsumableClass GetConsumable() {return null;}
}