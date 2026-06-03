using UnityEngine; 
using System.Collections.Generic; 
using System.IO; 

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath; 
    public ItemDatabaseObject database; 
    public List<InventorySlot> Container = new List<InventorySlot>();

    public void AddItem(ItemObject _item, int _amount)
    {
        for(int i = 0; i < Container.Count; i++)
        {
            if(Container[i].item == _item)
            {
                Container[i].AddAmount(_amount); 
                return; 
            }
        }
        
        Container.Add(new InventorySlot(database.GetId[_item], _item, _amount)); 
    }

    public void Save()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, savePath);
        string saveData = JsonUtility.ToJson(this, true);
        File.WriteAllText(fullPath, saveData);
        Debug.Log("Saved to: " + fullPath);
    }

    public void Load()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, savePath);
        if(File.Exists(fullPath))
        {
            string saveData = File.ReadAllText(fullPath);
            JsonUtility.FromJsonOverwrite(saveData, this);
            Debug.Log("Loaded from: " + fullPath);
        }
        else
        {
            Debug.LogWarning("No save file found at: " + fullPath);
        }
    }

    public void OnAfterDeserialize()
    {
        for(int i = 0; i < Container.Count; i++)
        {
            Container[i].item = database.GetItem[Container[i].ID]; 
        }
    }

    public void OnBeforeSerialize()
    {
        
    }
}

[System.Serializable]
public class InventorySlot
{
    public int ID; 
    public ItemObject item; 
    public int amount; 

    public InventorySlot(int _id, ItemObject _item, int _amount)
    {
        ID = _id; 
        item = _item; 
        amount = _amount; 
    }

    public void AddAmount(int value)
    {
        amount += value; 
    }
}