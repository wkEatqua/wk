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
            bonusStats.Add(bonusStat);
        }

        public virtual void Start()
        {
            if (curHp == 0) curHp = MaxHp;
        }
        public virtual void Die()
        {
            Destroy(gameObject);
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