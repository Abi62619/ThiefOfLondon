using UnityEngine;

[CreateAssetMenu(fileName = "New Tool Object", menuName = "Inventory System/Items/Tool")]
public class ToolObject : ItemObject
{
    public int coinValue; 

    public void Awake()
    {
        type = ItemType.Tool; 
    }
}