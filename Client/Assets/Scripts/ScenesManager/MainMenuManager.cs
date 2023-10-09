using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

/// <summary>
///  메인로비(수집 캐릭터들 상호작용),
///  다양한 메뉴로 이동할 수 있는 창,
///  셋팅 창
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    [Header(" ======= [ INFO ] ========")]
    public Image ImgProfile;
    public TextMeshProUGUI textProfile;
    
    [Header(" ======= [ POPUP ] ========")]
    public Image talkBalloon;


    //프로필 업로드
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
