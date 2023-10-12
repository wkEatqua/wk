using Shared.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    static bool dataloaded;
    protected override void Awake()
    {
        base.Awake();
        if (!dataloaded)
        {
            DataManager.Load(Application.dataPath+"/");
            
            dataloaded = true;
        }
    }
}
