using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Epos
{
    public abstract partial class Actor : TileObject
    {      
        protected virtual void Awake()
        {
            InitStatStrategy();
            baseStat.Init();
            bonusStats.Add(bonusStat);
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
            int dmg = Atk;
            float rand = Random.Range(0, 100);

            if(rand <= CritProb)
            {
                dmg =(int)(dmg * CritProb / 100f);
            }
            dmg = target.OnHit(dmg);
            return dmg;
        }        
    }
}