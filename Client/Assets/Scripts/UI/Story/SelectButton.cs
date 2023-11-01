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
    [HideInInspector] public bool isDiced;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => { OnClick.Invoke(this); });       
    }

    private void OnEnable()
    {
        isDiced = false;
    }
    public void DisablePointer()
    {
        isDisabled = true;
    }
    public void EnablePointer()
    {
        isDisabled = false;
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

    [HideInInspector] public UnityEvent<SelectButton> OnClick = new();   
}
