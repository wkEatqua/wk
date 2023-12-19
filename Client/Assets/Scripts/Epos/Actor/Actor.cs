using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Shared.Model;

namespace Epos
{
    public abstract partial class Actor : TileObject
    {      
        protected virtual void Awake()
        {
            InitStatStrategy();
            baseStat.Init();            
        }

        public virtual void Start()
        {
        }       

        public virtual void OnEnable()
        {
            if (curHp == 0) curHp = MaxHp;
        }
        protected virtual void OnDestroy()
        {
            if(tile != null)
            {
                tile.SetObject(null);
            }
        }
        public abstract int OnHit(int dmg);
        
        public virtual int Attack(Actor target)
        {
            EventInfo info = new EventInfo();
            ExcuteEvent(BuffEventType.OnMeleeAttack, info);

            bonusStat += info.stat;
            int dmg = Atk;
            float rand = Random.Range(0, 100);

            if(rand <= CritProb)
            {
                dmg =(int)(dmg * CritProb / 100f);
            }

            dmg *= 100 + Damage;
            dmg /= 100;

            dmg = target.OnHit(dmg);
            bonusStat -= info.stat;
            return dmg;
        }        
    }
}