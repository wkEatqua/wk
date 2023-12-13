using Epos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos.Buff
{
    public abstract partial class Buff
    {
        
        public Define.BuffEventType UseCondition;
        public int Amount;
        public Define.ActorStatType StatType;
        public Define.ValueType ValueType;

        protected Actor actor;

        protected bool isEquiped = false;
        
        public Buff()
        {
            isEquiped = false;
        }

        protected abstract IUseBuff UseStrategy { get; }
        protected abstract ICancelBuff CancelStrategy { get; }
        public virtual void OnEquip(Actor actor)
        {
            isEquiped = true;
            this.actor = actor;
            if (UseCondition == Define.BuffEventType.None)
            {
                UseStrategy.UseBuff();
            }
            else
            {
                actor.AddEvent(UseCondition, Invoke);
            }
        }

        public virtual void OnUnEquip()
        {
            if (!isEquiped) return;
            isEquiped = false;

            if (UseCondition == Define.BuffEventType.None)
            {
                CancelStrategy.CancelBuff();
            }
            else
            {
                actor.RemoveEvent(UseCondition, Invoke);
            }
        }      

        protected abstract void Invoke(BuffEvent.EventInfo info); 
           
    }
}