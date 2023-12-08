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

        public List<MeleeWeapon> MeleeWeapons => meleeWeapons;
        public List<Armour> Armours => armours;              
    
        public override int OnHit(int dmg)
        {
            int value = Def;
            armours.ForEach(x => value += x.durability);

            dmg -= value;

            if (dmg < 0) dmg = 0;

            foreach (Armour armour in armours)
            {
                int dur = armour.durability;
                armour.count--;
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

            armours = armours.Where(x => x.durability > 0 && x.count > 0).ToList();

            if (dmg < 0) dmg = 0;

            CurHp -= dmg;

            return dmg;
        }
        public override int Attack(Actor target)
        {
            int hp = target.CurHp;

            int dmg = Atk;
            float rand = Random.Range(0, 100);
            meleeWeapons.ForEach(x => dmg += x.durability);

            if (rand <= CritProb)
            {
                dmg = (int)(dmg * CritProb / 100f);
            }
            dmg = target.OnHit(dmg);
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

            return dmg;
        }

        public int RangeAttack(Actor target)
        {
            return base.Attack(target);
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
            EposManager.Instance.Player = this;
        }
        private void Update()
        {
        }
    }
}