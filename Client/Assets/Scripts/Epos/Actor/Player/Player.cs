using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos
{
    public class Player : Actor
    {
        public readonly List<MeleeWeapon> meleeWeapons = new();
        
        public override int Atk
        {
            get
            {
                int value = base.Atk;

                meleeWeapons.ForEach(x => value += x.durability);

                return value;
            }
        }
        public override int OnHit(int dmg)
        {
            dmg -= Def;

            if (dmg < 0) dmg = 0;

            CurHp -= dmg;

            return dmg;
        }
        public override int Attack(Actor target)
        {
            int hp = target.CurHp;

            int dmg = base.Attack(target);
            List<MeleeWeapon> removes = new();

            foreach(var weapon in meleeWeapons)
            {
                weapon.durability -= hp;            
                if(weapon.durability <= 0)
                {
                    removes.Add(weapon);
                }
            }

            removes.ForEach(weapon => meleeWeapons.Remove(weapon));

            return dmg;
        }
    }
}