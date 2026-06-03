using UnityEngine;

public class Pickpocket : MonoBehaviour
{
    [Header("References")]
    public ItemDatabaseObject database;
    public InventoryObject playerInventory;

    [Header("Settings")]
    public float pickpocketRange = 2f;
    public KeyCode pickpocketKey = KeyCode.F;

    void Update()
    {
        if (Input.GetKeyDown(pickpocketKey))
            TryPickpocket();
    }

    void TryPickpocket()
    {
        // Find nearest NPC in range
        Collider[] hits = Physics.OverlapSphere(transform.position, pickpocketRange);

        foreach (var hit in hits)
        {
            var target = hit.GetComponent<PickpocketTarget>();
            if (target == null || target.hasBeenPickpocketed) continue;

            // Roll against the NPC's loot chance
            if (Random.value <= target.lootChance)
            {
                ItemObject randomItem = GetRandomItem();
                if (randomItem != null)
                {
                    int amount = Random.Range(1, 4); // adjust range as needed
                    playerInventory.AddItem(randomItem, amount);
                    target.hasBeenPickpocketed = true;
                    Debug.Log($"Pickpocketed {target.name}: got {amount}x {randomItem.name}");
                }
            }
            else
            {
                Debug.Log($"Pickpocket failed on {target.name}!");
                // hook in detection / guard alert here
            }

            break; // only attempt one NPC per press
        }
    }

    ItemObject GetRandomItem()
    {
        if (database.Items.Length == 0) return null;

        int randomIndex = Random.Range(0, database.Items.Length);
        return database.Items[randomIndex];
    }

    ItemObject GetRandomItem(PickpocketTarget target)
    {
        // Use NPC-specific loot if defined
        ItemObject specific = target.GetRandomSpecificItem();
        if (specific != null) return specific;

        // Otherwise fall back to full database
        if (database.Items.Length == 0) return null;
        return database.Items[Random.Range(0, database.Items.Length)];
    }
}