using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Shared.Model;
using UnityEditor.Tilemaps;

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
            EventInfo info = new EventInfo();
            ExcuteEvent(Shared.Model.EventTypes.OnMeleeAttack, info);

            bonusStat += info.stat;
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
            bonusStat -= info.stat;
            return dmg;
        }

        public int RangeAttack(Actor target)
        {
            EventInfo info = new();
            ExcuteEvent(Shared.Model.EventTypes.OnRangeAttack, info);

            bonusStat += info.stat;
            int dmg = Atk;
            float rand = Random.Range(0, 100);

            if (rand <= CritProb)
            {
                dmg = (int)(dmg * CritProb / 100f);
            }

            dmg *= 100 + Damage;
            dmg /= 100;

            dmg = target.OnHit(dmg);
            bonusStat -= info.stat;
            return dmg;
        }
        public void Equip(MeleeWeapon wp)
        {
            meleeWeapons.Add(wp);
            OnStatChange.Invoke();
        }
        public void Equip(Armour armour)
        {
            armours.Add(armour);
            OnStatChange.Invoke();
        }
        protected override void Awake()
        {
            base.Awake();
            BonusStatEvent += () =>
            {
                BonusStat<ActorStatType> b = new();
                MeleeWeapons.ForEach(wp => b.AddValue(ActorStatType.Atk, wp.Durability));
                return b;
            };
            BonusStatEvent += () =>
            {
                BonusStat<ActorStatType> b = new();
                Armours.ForEach(wp => b.AddValue(ActorStatType.Def, wp.Durability));
                return b;
            };
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