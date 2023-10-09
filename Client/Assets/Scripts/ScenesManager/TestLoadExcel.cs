using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Shared.Data;

public class TestLoadExcel : MonoBehaviour
{
    public TextMeshProUGUI tText;

    public void Start()
    {
        DataManager.Load("");
    }
}
