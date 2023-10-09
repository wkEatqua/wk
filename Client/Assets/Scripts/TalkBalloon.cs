using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TalkBallon : MonoBehaviour
{
    TextMeshProUGUI text;
    Image balloonBG;

    private void Awake()
    {
        balloonBG = GetComponent<Image>();
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        balloonBG.color = new Color(255,255, 255, 255);
        text.color = new Color(0, 0, 0, 255);

        Invoke("showTalkBalloon", 5.0f);
    }

    //말풍선 fade out
    static Sequence sequenceFadeInOut;
    float Time = 5.0f;
    public void showTalkBalloon()
    {
        sequenceFadeInOut = DOTween.Sequence()
           .SetAutoKill(false) // DoTween Sequence는 기본적으로 일회용임. 재사용하려면 써주자.
           .OnRewind(() => // 실행 전. OnStart는 unity Start 함수가 불릴 때 호출됨. 낚이지 말자.
           {
               //talkBalloon.gameObject.SetActive(true);
           })
           .Append(balloonBG.DOFade(0.0f, Time)) // 어두워짐. 알파 값 조정.
                                                 //.Append(talkBalloon.DOFade(0.0f, Time)) // 밝아짐. 알파 값 조정.
           .OnComplete(() => // 실행 후.
           {
               gameObject.SetActive(false);
           });
    }

    public void Update()
    {
        text.color = new Color(0,0,0, balloonBG.color.a);
    }


   

}
