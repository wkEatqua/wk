using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos
{
    public class Player : Actor
    {
        public override float OnHit(float dmg)
        {
            dmg -= Def;

            if (dmg < 0) dmg = 0;

            CurHp -= dmg;

            return dmg;
        }
    }
}