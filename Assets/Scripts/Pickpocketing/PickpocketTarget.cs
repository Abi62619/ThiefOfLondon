using UnityEngine;

public class PickpocketTarget : MonoBehaviour
{
    [SerializeField] private string npcName;

    [Header("Pickpocket Loot")]
   
    [Range(0,1)] public float lootChance = 0.5f;  // chance THIS NPC drops item

    public bool hasBeenPickpocketed;
}
