using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos
{
    public class ArmourObject : ItemObject
    {
        readonly Armour armour = new (3,3);

        public override void Collect()
        {
            EposManager.Instance.Player.Equip(armour);
            base.Collect();
        }
    }

    public class Armour
    {
        public int durability;
        public int count;
        public Armour(int durability, int count)
        {
            this.durability = durability;
            this.count = count;
        }
    }
}