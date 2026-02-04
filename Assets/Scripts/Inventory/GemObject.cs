using UnityEngine; 

[CreateAssetMenu(fileName = "New Gem Object", menuName = "Inventory System/Items/Gem")]

public class GemObject : ItemObject 
{
    public GameObject GemItem; 

    public void Awake ()
    {
        type = ItemType.Gems; 
    }
}