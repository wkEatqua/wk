using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using System.Linq;
using System.Reflection;

public class SaveManager
{
    private readonly string devJsonFilePath = "Assets/Resources/Database/Save/";
    private readonly string distJsonFilePath = "WinterSpring/";

    PermanantData permanant = new();
    TempData temp = new();

    public PermanantData Permanant => permanant;
    public TempData Temp => temp;

    readonly GameSaveDatas gameDatas = new();

    public GameSaveDatas GameDatas => gameDatas;

    public void SaveAllDatas(int index)
    {
        gameDatas.SaveAll(index);
        Save(permanant, index);
        Save(temp, index);
    }

    public void LoadAllDatas(int index)
    {
        permanant = Load<PermanantData>(index);
        permanant ??= new();
        temp = Load<TempData>(index);
        temp ??= new();

        gameDatas.LoadAll(index);

    }
    void Save<T>(T data, int index) where T : SaveData
    {
        // Convert the GameData object to JSON string
        string json = data.ToJson();
#if UNITY_EDITOR
        File.WriteAllText(Path.Combine(devJsonFilePath, $"{index}/{typeof(T).Name}.json"), json);
#else
        // Save the JSON string to a file
        File.WriteAllText(Path.Combine(Application.persistentDataPath, distJsonFilePath, $"{index}/{typeof(T).Name}.json"), json);     
#endif
    }

    T Load<T>(int index) where T : SaveData
    {
        string json = "";

#if UNITY_EDITOR
        TextAsset jsonFile = Resources.Load<TextAsset>(Path.Combine("Database/Save/", $"{index}/ {typeof(T).Name}"));

        if (jsonFile != null)
        {
            json = jsonFile.text;
        }

#else
        string path = Path.Combine(Application.persistentDataPath, distJsonFilePath, $"{index}/ {typeof(T).Name}");

        if (File.Exists(path))
        {
            // Read the JSON string from the file
            json = File.ReadAllText(path);           
        }
        else
        {
            return null;
        }
#endif

        return JsonUtility.FromJson<T>(json);
    }

    [System.Serializable]
    public class SaveData
    {
        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
    [System.Serializable]
    public class PermanantData : SaveData
    {
             
    }
    [System.Serializable]

    public class TempData : SaveData
    {

    }
}


