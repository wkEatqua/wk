using Shared.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public TextMeshProUGUI tmp2;
    Button button;

    public ScenarioSelectInfo info;

    bool isDisabled;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => { OnClick.Invoke(info); });
        button.onClick.AddListener(OnClick2.Invoke);       
    }

    public void DisablePointer()
    {
        isDisabled = true;
    }
    public void OnPointerEnter()
    {
        if(isDisabled) return;
        tmp.fontStyle = FontStyles.Underline;
    }

    public void OnPointerExit()
    {
        if (isDisabled) return;

        tmp.fontStyle = FontStyles.Normal;
    }

    [HideInInspector] public UnityEvent<ScenarioSelectInfo> OnClick = new();
    [HideInInspector] public UnityEvent OnClick2 = new();
}
