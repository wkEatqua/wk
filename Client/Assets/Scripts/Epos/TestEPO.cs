using Epos;
using Shared.Data;
using Shared.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestEPO : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    private void Start()
    {
        EposManager.Instance.Player.OnStatChange.AddListener(() => tmp.text = EposManager.Instance.Player.Def.ToString());
        
        EposItem data = new()
        {
            BaseStat = 10,
        };
        EposItemInfo info = new(data);
        Armour melee = new(info, null);
        EposManager.Instance.Player.Equip(melee);
    }
}
