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

    //��ǳ�� fade out
    static Sequence sequenceFadeInOut;
    float Time = 5.0f;
    public void showTalkBalloon()
    {
        sequenceFadeInOut = DOTween.Sequence()
           .SetAutoKill(false) // DoTween Sequence�� �⺻������ ��ȸ����. �����Ϸ��� ������.
           .OnRewind(() => // ���� ��. OnStart�� unity Start �Լ��� �Ҹ� �� ȣ���. ������ ����.
           {
               //talkBalloon.gameObject.SetActive(true);
           })
           .Append(balloonBG.DOFade(0.0f, Time)) // ��ο���. ���� �� ����.
                                                 //.Append(talkBalloon.DOFade(0.0f, Time)) // �����. ���� �� ����.
           .OnComplete(() => // ���� ��.
           {
               gameObject.SetActive(false);
           });
    }

    public void Update()
    {
        text.color = new Color(0,0,0, balloonBG.color.a);
    }


   

}
