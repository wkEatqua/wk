using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    Stack<UI_Popup> _popups = new Stack<UI_Popup>();

    int _order = 10;

    UI_Scene _sceneUI = null;


    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        //Input.GetKeyDown(KeyCode.Escape)
        //Input.GetKeyDown(KeyCode.Escape);

        _sceneUI = FindAnyObjectByType<UI_Scene>();
    }

    public UI_Scene GetSceneUI()
    {
        return _sceneUI;
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Utils.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public void ShowPopup<T>() where T : UI_Popup
    {
        var Popup = GetComponentInChildren<T>();

        if (Popup == null)
            return;

        Popup.gameObject.SetActive(true);
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
