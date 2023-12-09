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
            armours.ForEach(x => value += x.Durability);

            dmg -= value;

            if (dmg < 0) dmg = 0;

            foreach (Armour armour in armours)
            {
                int dur = armour.Durability;
                armour.Count--;
                if (dur >= dmg)
                {
                    armour.Durability -= dmg;
                    break;
                }
                else
                {
                    armour.Durability = 0;
                    dmg -= dur;
                }
            }

            armours = armours.Where(x => x.Durability > 0 && x.Count > 0).ToList();

            if (dmg < 0) dmg = 0;

            CurHp -= dmg;

            return dmg;
        }
        public override int Attack(Actor target)
        {
            int hp = target.CurHp;

            int dmg = Atk;
            float rand = Random.Range(0, 100);
            meleeWeapons.ForEach(x => dmg += x.Durability);

            if (rand <= CritProb)
            {
                dmg = (int)(dmg * CritProb / 100f);
            }
            dmg *= 100 + Damage;
            dmg /= 100;
            dmg = target.OnHit(dmg);

            int durabilityMinus = Mathf.Min(dmg, hp);
            foreach (MeleeWeapon wp in meleeWeapons)
            {
                int dur = wp.Durability;
                if (dur >= durabilityMinus)
                {
                    wp.Durability -= durabilityMinus;
                    break;
                }
                else
                {
                    wp.Durability = 0;
                    durabilityMinus -= dur;
                }
            }
            meleeWeapons = meleeWeapons.Where(wp => wp.Durability > 0).ToList();

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