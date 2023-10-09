using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryLobbyManager : MonoBehaviour
{

    public void OnClickBackBtn()
    {
        SceneChangeManager.instance.SceneMove("MainLobby");
    }

    public void OnClickMainStory()
    {
        SceneChangeManager.instance.SceneMove("MainStory");
    }
}
