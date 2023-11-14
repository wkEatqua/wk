using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public abstract class SaveData
{
    protected GameSaveDatas gameData;
    public bool isLoaded = false;
    public SaveData(GameSaveDatas gameData)
    {
        this.gameData = gameData;
        isLoaded = false;
    }
    public abstract void Save();
    public abstract void Load();
}
public class GameSaveDatas
{
    List<SaveData> datas;

    public List<SaveData> Datas
    {
        get
        {
            if (datas == null)
            {
                var types = TypeCache.GetTypesDerivedFrom(typeof(SaveData)).Where(x => x.IsSubclassOf(typeof(SaveData)));
                datas = new List<SaveData>();

                foreach (var type in types)
                {
                    datas.Add(Activator.CreateInstance(type,this) as SaveData);
                }
            }

            return datas;
        }
    }

    readonly SaveManager manager;
    public SaveManager Manager => manager;
    public GameSaveDatas(SaveManager manager)
    {
        this.manager = manager;
    }
    public void SaveAll()
    {        
        foreach (var data in Datas)
        {
            data.Save();
        }
    }

    public void LoadAll()
    {
        foreach (var data in Datas)
        {
            data.Load();
        }
    }
}
