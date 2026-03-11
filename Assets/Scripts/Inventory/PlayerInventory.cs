using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public ItemDatabase database;

    public InventoryData GetSaveData()
    {
        InventoryData data = new InventoryData();

        foreach (var item in items)
        {
            data.itemIds.Add(item.itemId);
        }

        return data;
    }

    public void LoadData(InventoryData data)
    {
        items.Clear();

        foreach (int id in data.itemIds)
        {
            Item item = database.GetItem(id);
            if (item != null)
                items.Add(item);
        }
    }
}