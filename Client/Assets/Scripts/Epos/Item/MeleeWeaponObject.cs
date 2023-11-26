using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos
{
    public class MeleeWeaponObject : ItemObject
    {
        readonly MeleeWeapon weapon = new(5);
        public override void Collect()
        {
            EposManager.Instance.Player.Equip(weapon);
            base.Collect();
        }
    }
    public class MeleeWeapon
    {
        public int durability;

        public MeleeWeapon(int durability)
        {
            this.durability = durability;
        }
    }
}