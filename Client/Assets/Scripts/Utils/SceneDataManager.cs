using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDataManager : MonoBehaviour
{ 

    public static float BGSoundValue;   //����� ����
    public static float EFSoundValue;   //ȿ���� ����

    public static float StoryFontValue; //���丮 ��Ʈ ũ��
    public static float RSValue;        //�ػ� 

    public static SceneDataManager instance = new SceneDataManager();
    public void Awake()
    {
        if (instance == null) instance = this;
        else instance = null;

        DontDestroyOnLoad(this.gameObject);
    }

}
