using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UI_Scene : UI_Base
{

    protected override void Init()
    {
        //UIManager.Instance.SetCanvas(gameObject, false);
        BindUI();
    }

    protected virtual void BindUI()
    {

    }
}
