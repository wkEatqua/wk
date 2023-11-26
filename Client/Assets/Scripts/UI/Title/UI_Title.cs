using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Title : MonoBehaviour
{
    [SerializeField]
    private GameObject LoadingImage;

    [SerializeField]
    private string NextSceneName = "";

    private void Start()
    {
        if (null != LoadingImage)
        {
            LoadingImage.SetActive(false);
        }
    }

    private void SceneChange()
    {
        SceneChangeManager.instance.SceneMove(NextSceneName);
    }

    public void OnClickStartButton()
    {
        LoadingImage.SetActive(true);

        // Debug Code
        Invoke("SceneChange", 1.0f);
        // Debug Code End
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
