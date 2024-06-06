using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

[Serializable]

public class SaveData
{
    public List<int> addID = new List<int>();
    public List<int> inventoryItemsAmount = new List<int>();
    public List<string> inventoryItemsName = new List<string>();
}




public class GameData : MonoBehaviour
{
    public SaveData saveData;

    public static GameData instance;

    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(instance.gameObject);
            instance = this;
        }
        if (File.Exists(Application.persistentDataPath + "/PlayerData.dat"))
        {
            Load();
        }
        else
        {
            Save();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/PlayerData.dat", FileMode.Create);
        SaveData data = new SaveData();
        data = saveData;
        formatter.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.dat"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PlayerData.dat", FileMode.Open);
            saveData = formatter.Deserialize(file) as SaveData;
            file.Close();
        }
    }

    public void ClearData()
    {
        if(File.Exists(Application.persistentDataPath + "/PlayerData.dat")){
            File.Delete(Application.persistentDataPath + "/PlayerData.dat");
        }
    }
    public void ClearAllDataList()
    {
        saveData.addID.Clear();
        saveData.inventoryItemsName.Clear();
        saveData.inventoryItemsAmount.Clear();
        Save();
    }
}
