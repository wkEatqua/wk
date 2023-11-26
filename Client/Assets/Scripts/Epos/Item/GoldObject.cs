using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos
{
    public class GoldObject : ItemObject
    {
        public int gold = 10;
        public override void Collect()
        {
            base.Collect();
            EposManager.Instance.gold += gold;
        }
    }
}