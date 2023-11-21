using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Title : MonoBehaviour
{
    public void OnClickStartButton()
    {

    }

    public void OnClickGrownButton()
    {

    }

    public void OnClickWikiButton()
    {

    }

    public void OnClickSettingButton()
    {

    }

    public void OnClickCreditButton()
    {

    }

    public void OnClickExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
