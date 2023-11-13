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

    List<SaveData> dataList = new();

    public List<SaveData> DataList => dataList;

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
        dataList.Clear();
        saveType = type;
        PlayerPrefs.SetString("SaveType", saveType.ToString());
        gameDatas.SaveAll();

        switch (type)
        {
            case SaveType.Story:
                dataList.ForEach(x =>
                {
                    if (x is StoryData)
                    {
                        Save(x as StoryData);
                    }
                });
                break;
            case SaveType.Minigame:
                dataList.ForEach(x =>
                {
                    if (x is MiniGameData)
                    {
                        Save(x as MiniGameData);
                    }
                });
                break;
        }
    }

    public void LoadAllDatas()
    {
        string str = PlayerPrefs.GetString("SaveType", "Null");
        
        if (Enum.IsDefined(typeof(SaveType), str))
        {
            saveType = (SaveType)Enum.Parse(typeof(SaveType), str);

            switch (saveType)
            {
                case SaveType.Story:
                    dataList = Load<StoryData>();
                    break;
                case SaveType.Minigame:
                    dataList = Load<MiniGameData>();
                    break;
            }

            dataList ??= new();
        }
        else
        {
            dataList.Clear();
        }
        
        gameDatas.LoadAll();
    }
    void Save<T>(T data) where T : SaveData
    {
        // Convert the GameData object to JSON string
        string json = data.ToJson();
#if UNITY_EDITOR
        File.WriteAllText(Path.Combine(devJsonFilePath, $"{typeof(T).Name}/SaveData.json"), json);
#else
        // Save the JSON string to a file
        File.WriteAllText(Path.Combine(Application.persistentDataPath, distJsonFilePath, $"{typeof(T).Name}.json"), json);     
#endif
    }

    List<SaveData> Load<T>() where T : SaveData
    {

#if UNITY_EDITOR
        TextAsset[] jsonFiles = Resources.LoadAll<TextAsset>(Path.Combine("JsonData/Save/", typeof(T).Name));

        if (jsonFiles == null)
        {
            return null;
        }

        List<SaveData> jsons = jsonFiles.Select(x => JsonUtility.FromJson<T>(x.text) as SaveData).ToList();
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

        return jsons;
    }

    [System.Serializable]
    public class SaveData
    {
        public long SaveId;
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


