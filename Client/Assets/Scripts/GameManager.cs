using Shared.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    static bool dataloaded;
    protected override void Awake()
    {
        base.Awake();
        if (!dataloaded)
        {
            DataManager.Load("C:\\Users\\kg881\\Perforce\\wk_program_4958\\");
            
            dataloaded = true;
        }
    }
}
