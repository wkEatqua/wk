using Shared.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    static bool dataloaded;

    public int scriptIndex = -1;

    static SaveManager save;
    public static SaveManager Save
    {
        get
        {
            save ??= new SaveManager();
            return save;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        scriptIndex = -1;
        if (!dataloaded)
        {
            DataManager.Load(Application.dataPath+"/");
            
            dataloaded = true;
        }
    }     
}
