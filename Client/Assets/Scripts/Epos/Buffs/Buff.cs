using Epos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shared.Model;

namespace Epos.Buff
{
    public abstract partial class Buff
    {
        
        public Shared.Model.EventTypes UseCondition;
        public int Amount;
        public ActorStatType StatType;
        public AddType ValueType;

        protected Actor actor;

        protected bool isEquiped = false;
        public int ConditionParam;
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
            if (UseCondition == Shared.Model.EventTypes.None)
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

            if (UseCondition == Shared.Model.EventTypes.None)
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