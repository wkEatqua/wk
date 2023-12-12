using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    Stack<UI_Popup> _popups = new Stack<UI_Popup>();

    int _order = 10;

    UI_Scene _sceneUI = null;


    private void Start()
    {
        //Input.GetKeyDown(KeyCode.Escape)
        //Input.GetKeyDown(KeyCode.Escape);
    }


    public void ShowPopup<T>() where T : UI_Popup
    {
        var Popup = GetComponentInChildren<T>();

        if (Popup == null)
            return;

        Popup.transform.SetParent(_sceneUI.transform);
    }
   
    public void ClosePopup()
    {
        if (_popups.Count == 0)
            return;

        var Popup = _popups.Pop();
        Popup.transform.SetParent(this.transform);
        Popup.gameObject.SetActive(false);
    }
}
