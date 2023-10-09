using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

/// <summary>
///  ���ηκ�(���� ĳ���͵� ��ȣ�ۿ�),
///  �پ��� �޴��� �̵��� �� �ִ� â,
///  ���� â
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    [Header(" ======= [ INFO ] ========")]
    public Image ImgProfile;
    public TextMeshProUGUI textProfile;
    
    [Header(" ======= [ POPUP ] ========")]
    public Image talkBalloon;


    //������ ���ε�
    public void profileUpload(Image img, string name)
    {
        ImgProfile = img;
        textProfile.text = name;
    }

   

    public void OnClickLibBtn()
    {
        SceneChangeManager.instance.SceneMove("LibraryLobby");
    }
  
}
