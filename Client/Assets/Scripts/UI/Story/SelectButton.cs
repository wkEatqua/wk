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
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => { OnClick.Invoke(info); });
    }
    public void OnPointerEnter()
    {
        tmp.fontStyle = FontStyles.Underline;
    }

    public void OnPointerExit()
    {
        tmp.fontStyle = FontStyles.Normal;
    }

    [HideInInspector] public UnityEvent<ScenarioSelectInfo> OnClick = new();
}
