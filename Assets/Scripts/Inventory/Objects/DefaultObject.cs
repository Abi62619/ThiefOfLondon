using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Default")]
public class DefaultObject : MonoBehaviour
{
    public void Awake()
    {
        type = ItemType.Default; 
    }
}
