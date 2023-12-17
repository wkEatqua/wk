using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shared.Data;
using TMPro;

public class TilingDebug : MonoBehaviour
{
    [SerializeField]
    TMP_InputField InputField;

    public void OnClickPlayerLevelButton()
    {
        int PlayerLevel = int.Parse(InputField.text);
    }

    public void OnClickCheckInfoButton()
    {
    }
}
