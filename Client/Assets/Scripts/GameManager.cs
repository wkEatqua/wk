using Shared.Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    static bool dataloaded;

    [HideInInspector] public int scriptIndex = -1;

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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("UI위임");
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.TryGetComponent(out IRaycast raycast))
                {
                    raycast.OnRayCast();
                }
            }
        }
    }
}
