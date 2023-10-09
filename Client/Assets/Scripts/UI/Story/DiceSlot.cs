using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numText;
    public int Num => num;

    int num;
    public void Init(int num)
    {
        numText.text = num.ToString();
        this.num = num;
    }
}
