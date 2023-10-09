using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDataManager : MonoBehaviour
{ 

    public static float BGSoundValue;   //배경음 조절
    public static float EFSoundValue;   //효과음 조절

    public static float StoryFontValue; //스토리 폰트 크기
    public static float RSValue;        //해상도 

    public static SceneDataManager instance = new SceneDataManager();
    public void Awake()
    {
        if (instance == null) instance = this;
        else instance = null;

        DontDestroyOnLoad(this.gameObject);
    }

}
