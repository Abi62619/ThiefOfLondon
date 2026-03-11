using UnityEngine;
using System.IO;

public static class InventorySaveSystem
{
    static string path = Application.persistentDataPath + "/inventory.json";

    public static void SaveInventory(PlayerInventory inventory)
    {
        InventoryData data = inventory.GetSaveData();

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(path, json);

        Debug.Log("Inventory saved to: " + path);
    }

    public static void LoadInventory(PlayerInventory inventory)
    {
        if (!File.Exists(path))
        {
            Debug.Log("No save file found");
            return;
        }

        string json = File.ReadAllText(path);

        InventoryData data = JsonUtility.FromJson<InventoryData>(json);

        inventory.LoadData(data);

        Debug.Log("Inventory loaded");
    }
}