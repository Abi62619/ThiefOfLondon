using UnityEngine;

[CreateAssetMenu(fileName = "New Coins", menuName = "Inventory System/Items/Coins")]
public class Coins : Item    
{   
    public void OnEnable()
    {
        itemType = ItemType.Coins; 
    }
}

[CreateAssetMenu(fileName = "New Necklace", menuName = "Inventory System/Items/Necklace")]
public class Necklace : Item    
{   
    public void OnEnable()
    {
        itemType = ItemType.Necklace; 
    }
}

[CreateAssetMenu(fileName = "New Bracelet", menuName = "Inventory System/Items/Bracelet")]
public class Bracelet : Item    
{   
    public void OnEnable()
    {
        itemType = ItemType.Bracelet; 
    }
}

[CreateAssetMenu(fileName = "New Handkerchiefs", menuName = "Inventory System/Items/Handkerchiefs")]
public class Handkerchiefs : Item    
{   
    public void OnEnable()
    {
        itemType = ItemType.Handkerchiefs; 
    }
}

[CreateAssetMenu(fileName = "New Purses", menuName = "Inventory System/Items/Purses")]
public class Purses : Item    
{   
    public void OnEnable()
    {
        itemType = ItemType.Purses; 
    }
} 