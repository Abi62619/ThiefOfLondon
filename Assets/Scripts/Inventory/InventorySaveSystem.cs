using UnityEngine;

public static class InventorySaveSystem
{
    private const string INVENTORY_KEY = "PLAYER_INVENTORY";

    public static void SaveInventory(PlayerInventory inventory)
    {
        InventoryData data = inventory.GetSaveData();

        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(INVENTORY_KEY, json);
        PlayerPrefs.Save();

        Debug.Log("Inventory saved with PlayerPrefs");
    }

    public static void LoadInventory(PlayerInventory inventory)
    {
        if (!PlayerPrefs.HasKey(INVENTORY_KEY))
        {
            Debug.Log("No inventory save found");
            return;
        }

        string json = PlayerPrefs.GetString(INVENTORY_KEY);

        InventoryData data = JsonUtility.FromJson<InventoryData>(json);

        inventory.LoadData(data);

        Debug.Log("Inventory loaded with PlayerPrefs");
    }
}