using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public abstract class SaveData
{
    public abstract void Save(int index);
    public abstract void Load(int index);
}
public class GameSaveDatas
{
    List<SaveData> datas;

    List<SaveData> Datas
    {
        get
        {
            if (datas == null)
            {
                var types = TypeCache.GetTypesDerivedFrom(typeof(SaveData)).Where(x => x.IsSubclassOf(typeof(SaveData)));
                datas = new List<SaveData>();

                foreach (var type in types)
                {
                    datas.Add(Activator.CreateInstance(type) as SaveData);
                }
            }

            return datas;
        }
    }  

    public enum DataType
    {
        
    }

    public void SaveAll(int index)
    {
        foreach (var data in Datas)
        {
            data.Save(index);
        }
    }

    public void LoadAll(int index)
    {
        foreach (var data in Datas)
        {
            data.Load(index);
        }
    }
}
