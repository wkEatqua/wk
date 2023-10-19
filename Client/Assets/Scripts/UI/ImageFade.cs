using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ImageFade : MonoBehaviour
{
    public float fadeTime;

    Image image;

    [Header("이벤트")]
    [Tooltip("페이드 인 끝날 시 이벤트")] public UnityEvent OnFadeIn = new();
    [Tooltip("페이드 아웃 끝날 시 이벤트")] public UnityEvent OnFadeOut = new();

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        FadeIn();
    }

    bool isFadeIn = false;
    bool isFadeOut = false;

    public void FadeIn()
    {
        StartCoroutine(nameof(FadeInCoroutine));
    }

    public void FadeOut()
    {
        StartCoroutine(nameof(FadeOutCoroutine));
    }

    public void SkipFadeIn()
    {
        if (isFadeIn)
        {
            StopCoroutine(nameof(FadeInCoroutine));
            Color color = image.color;
            color.a = 1f;
            image.color = color;
            OnFadeIn.Invoke();
        }
    }

    public void SkipFadeOut()
    {
        if (isFadeOut)
        {
            StopCoroutine(nameof(FadeOutCoroutine));
            Color color = image.color;
            color.a = 0f;
            image.color = color;
            OnFadeOut.Invoke();
        }
    }
    IEnumerator FadeInCoroutine()
    {
        if (!isFadeIn)
        {
            isFadeIn = true;
            Color color = image.color;
            color.a = 0;
            image.color = color;
            float curTime = 0;

            while (curTime < fadeTime)
            {
                color.a = Mathf.Lerp(0, 1, curTime / fadeTime);
                image.color = color;
                yield return new WaitForEndOfFrame();
                curTime += Time.deltaTime;
            }
            color.a = 1;

            OnFadeIn.Invoke();
            isFadeIn = false;
        }
    }

    IEnumerator FadeOutCoroutine()
    {
        if (!isFadeOut)
        {
            isFadeOut = true;
            Color color = image.color;

            float curTime = 0;

            while (curTime < fadeTime)
            {
                color.a = Mathf.Lerp(1, 0, curTime / fadeTime);
                image.color = color;
                yield return new WaitForEndOfFrame();
                curTime += Time.deltaTime;
            }
            color.a = 0;

            OnFadeOut.Invoke();
            isFadeOut = false;
        }
    }

}
