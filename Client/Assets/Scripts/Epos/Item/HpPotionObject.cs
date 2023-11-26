using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos
{
    public class HpPotionObject : ItemObject
    {
        public int hpValue;

        public override void Collect()
        {
            base.Collect();
            EposManager.Instance.Player.CurHp += hpValue;
        }
    }
}