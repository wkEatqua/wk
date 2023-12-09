using Epos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos.Buff
{
    public abstract partial class Buff
    {
        
        public BuffEventType UseType;
        public int Amount;
        public ActorStatType StatType;
        public StatType AddType;

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
            if (UseType == BuffEventType.None)
            {
                UseStrategy.UseBuff();
            }
            else
            {
                actor.AddEvent(UseType, Invoke);
            }
        }

        public virtual void OnUnEquip()
        {
            if (!isEquiped) return;
            isEquiped = false;

            if (UseType == BuffEventType.None)
            {
                CancelStrategy.CancelBuff();
            }
            else
            {
                actor.RemoveEvent(UseType, Invoke);
            }
        }      

        protected abstract void Invoke(BuffEvent.EventInfo info); 
           
    }
}