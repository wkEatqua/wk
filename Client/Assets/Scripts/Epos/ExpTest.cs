using Epos;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpTest : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI level;
    public TextMeshProUGUI exp;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() => EposManager.Instance.Exp += 50);
    }

    // Update is called once per frame
    void Update()
    {
        level.text = EposManager.Instance.level.ToString();
        exp.text = EposManager.Instance.Exp + "/" + EposManager.Instance.MaxExp;
    }
}
