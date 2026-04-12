using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory System/Items/Weapon")]
public class WeaponObject : ItemObject
{
    public int coinValue; 

    public void Awake()
    {
        type = ItemType.Weapon; 
    }
}