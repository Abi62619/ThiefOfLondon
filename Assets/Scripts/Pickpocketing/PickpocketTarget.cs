using UnityEngine;

public class PickpocketTarget : MonoBehaviour
{
    [SerializeField] private string npcName;

    [Header("Pickpocket Loot")]
    [Range(0, 1)] public float lootChance = 0.5f;

    [Header("Override if fail")]
    public ItemObject[] specificItems; // drag in items in Inspector

    public bool hasBeenPickpocketed;

    // Returns null if no override, Pickpocket script falls back to full database
    public ItemObject GetRandomSpecificItem()
    {
        if (specificItems == null || specificItems.Length == 0) return null;
        return specificItems[Random.Range(0, specificItems.Length)];
    }
}