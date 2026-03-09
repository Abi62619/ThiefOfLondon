using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "Inventory System/Items")]

public class Coins : Item    
{   
    public void OnEnable()
    {
        type = itemType.Coins; 
    }
}

public class Necklace : Item    
{   
    public void OnEnable()
    {
        type = itemType.Necklace; 
    }
}

public class Bracelet : Item    
{   
    public void OnEnable()
    {
        type = itemType.Bracelet; 
    }
}

public class Handkerchiefs : Item    
{   
    public void OnEnable()
    {
        type = itemType.Handkerchiefs; 
    }
}

public class Purses : Item    
{   
    public void OnEnable()
    {
        type = itemType.Purses; 
    }
} 