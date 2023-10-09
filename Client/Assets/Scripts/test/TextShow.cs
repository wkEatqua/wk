using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextShow : MonoBehaviour
{
    public Transform parents;


    void Test()
    {
        //Instantiate(StoryTellDataManager.instance().go[StoryTellDataManager.instance.count], parents);
    }







    /// <summary>
    /// TEST
    /// </summary>
    public void OnTextShowed()
    {
        Debug.Log("OnTextShowed");
    }
    public void OnTextDisappeared()
    {
        Debug.Log("OnTextDisappeared");
    }
    public void OnTypewriterStart()
    {
        Debug.Log("OnTypewriterStart");
    }
    public void OnCharacterVisible()
    {
        Debug.Log("OnCharacterVisible");
    }
    public void OnMessage()
    {
        Debug.Log("OnMessage");
        Test();
       // count++;
    }
}
