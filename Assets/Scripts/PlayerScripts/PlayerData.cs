using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

[Serializable]
public class PlayerData
{
    public List<InventoryItemData> InventoryItems = new List<InventoryItemData>();
    public List<InventoryItemData> EquipmentItems = new List<InventoryItemData>();

    // Save player data as JSON
    public void SavePlayerDataFile()
    {
        string json = JsonUtility.ToJson(this);
        File.WriteAllText(Application.persistentDataPath + $"/data.json", json);
    }

    // Load player data from JSON
    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + $"/data.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);
        }
        else
        {
            Debug.LogWarning("Player data file not found.");
        }
    }

    public void DeletePlayerDataFile()
    {
        string path = Application.persistentDataPath + $"/data.json";
        File.Delete(path);
        Debug.Log("Deleted save file");
    }

    public bool CheckForExistingData()
    {
        string path = Application.persistentDataPath + $"/data.json";

        return File.Exists(path);
    }
}