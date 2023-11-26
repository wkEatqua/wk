using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Epos
{
    public class Player : Actor
    {
        List<MeleeWeapon> meleeWeapons = new();
        List<Armour> armours = new();

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

            foreach (Armour armour in armours)
            {
                int dur = armour.durability;
                if (dur >= dmg)
                {
                    armour.durability -= dmg;
                    break;
                }
                else
                {
                    armour.durability = 0;
                    dmg -= dur;
                }
            }

            armours = armours.Where(x => x.durability > 0).ToList();

            if (dmg < 0) dmg = 0;

            CurHp -= dmg;

            return dmg;
        }
        public override int Attack(Actor target)
        {
            int hp = target.CurHp;

            int dmg = base.Attack(target);

            int durabilityMinus = Mathf.Min(dmg, hp);
            foreach (MeleeWeapon wp in meleeWeapons)
            {
                int dur = wp.durability;
                if (dur >= durabilityMinus)
                {
                    wp.durability -= durabilityMinus;
                    break;
                }
                else
                {
                    wp.durability = 0;
                    durabilityMinus -= dur;
                }
            }
            meleeWeapons = meleeWeapons.Where(wp => wp.durability > 0).ToList();

            if(target != null && target.gameObject.activeSelf && target.CurHp > 0)
            {
                OnHit(target.CurHp);
            }
            return dmg;
        }

        public void Equip(MeleeWeapon wp)
        {
            meleeWeapons.Add(wp);
        }
        public void Equip(Armour armour)
        {
            armours.Add(armour);
        }
        public override void Start()
        {
            base.Start();
        }             
    }
}