using Apis;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DmgTextShow
{
    static AddressablePooling pool;
    static AddressablePooling Pool
    {
        get
        {
            pool ??= new AddressablePooling("Dmg",true);
            return pool;
        }
    }
    public static void ShowDmg(Vector3 position, Color color,float dmg)
    {
        GameObject text = Pool.Get("DmgText");

        text.transform.position = position + new Vector3(0, 0, -2);
        TextMeshPro tmp = text.GetComponent<TextMeshPro>();

        tmp.color = color;
        tmp.text = dmg.ToString();
        text.transform.DOMoveY(text.transform.position.y + 1, 1f).OnComplete(() => Pool.Return(text));
    }
}
