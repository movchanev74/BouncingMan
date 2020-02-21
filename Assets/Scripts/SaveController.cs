using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    public BouncingMan BouncingMan;
    public SettingsController SettingsController;

    private string savePath;

    public void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "save.dat");
    }

    public void Save()
    {
        SaveData save = new SaveData
        {
            BouncingManSaveData = BouncingMan.GetSaveData(),
            JumpSettingNumber = SettingsController.GetSettingNumber()
        };
        SaveDataToDisk(save);
    }

    public void Load()
    {
        var save = LoadDataFromDisk();
        if(save != null)
        {
            SettingsController.SetJumpSetting(save.JumpSettingNumber);
            BouncingMan.LoadSaveData(save.BouncingManSaveData);
        }
    }

    private void SaveDataToDisk(SaveData saveData)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(savePath);
        binaryFormatter.Serialize(file, saveData);
        file.Close();
    }

    private SaveData LoadDataFromDisk()
    {
        if (File.Exists(savePath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);
            var saveData = (SaveData)binaryFormatter.Deserialize(file);
            file.Close();
            return saveData;
        }
        else
        {
            Debug.Log("SaveData not found.");
        }
        return null;
    }
}

[System.Serializable]
public class SaveData
{
    [SerializeField]
    public BouncingManSaveData BouncingManSaveData;
    [SerializeField]
    public int JumpSettingNumber;
}