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
    private readonly string devJsonFilePath = "Assets/Resources/JsonData/Save/";
    private readonly string distJsonFilePath = "wk/";

    SaveData data = new();

    public SaveData Data => data;

    readonly GameSaveDatas gameDatas;

    public GameSaveDatas GameDatas => gameDatas;

    public enum SaveType
    {
        Story, Minigame
    }
    SaveType saveType;
    public SaveManager()
    {
        gameDatas = new(this);
        LoadAllDatas();
    }
    public void SaveAllDatas(SaveType type)
    {
        switch (type)
        {
            case SaveType.Story:
                data = new StoryData();
                break;
            case SaveType.Minigame:
                data = new MiniGameData();
                break;
        }
        saveType = type;
        PlayerPrefs.SetString("SaveType", saveType.ToString());
        gameDatas.SaveAll();

        
        Save(data);
    }

    public void LoadAllDatas()
    {
        string str = PlayerPrefs.GetString("SaveType");
       
        if (Enum.IsDefined(typeof(SaveType), str)) 
        {
            saveType = (SaveType)Enum.Parse(typeof(SaveType), str);
            
            switch (saveType)
            {
                case SaveType.Story:
                    data = Load<StoryData>();
                    if (data != null) data.IsLoaded = true;
                    else data = new StoryData();

                    break;
                case SaveType.Minigame:
                    data = Load<MiniGameData>();
                    if (data != null) data.IsLoaded = true;
                    else data = new MiniGameData();
                    break;
            }
        }
        else
        {
            data = new SaveData();
        }

        gameDatas.LoadAll();
    }
    void Save<T>(T data) where T : SaveData
    {
        // Convert the GameData object to JSON string
        string json = data.ToJson();
#if UNITY_EDITOR
        File.WriteAllText(Path.Combine(devJsonFilePath, $"{typeof(T).Name}.json"), json);
#else
        // Save the JSON string to a file
        File.WriteAllText(Path.Combine(Application.persistentDataPath, distJsonFilePath, $"{typeof(T).Name}.json"), json);     
#endif
    }

    T Load<T>() where T : SaveData
    {
        string json = "";

#if UNITY_EDITOR
        TextAsset jsonFile = Resources.Load<TextAsset>(Path.Combine("JsonData/Save/","SaveData"));
        
        if (jsonFile != null)
        {
            json = jsonFile.text;
        }

#else
        string path = Path.Combine(Application.persistentDataPath, distJsonFilePath, "SaveData");

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
        public bool IsLoaded { get; set; }
        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
    [System.Serializable]
    public class StoryData : SaveData
    {
        public long ChapterId;
        public long WorldId;
        public int Stamina;
        public int Page;
        public int CurSelectedVeris;
        public int CurMaxVeris;
        public int Hp;
    }
    public class MiniGameData : SaveData
    {

    }
}


