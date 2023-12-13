using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos.Buff
{
    public partial class Buff 
    {
        #region IUseBuff
        public interface IUseBuff
        {
            void UseBuff();
        }      
        public class NothingUse : IUseBuff
        {
            public void UseBuff()
            {
            }
        }              
        public class AddStat : IUseBuff
        {
            readonly Buff buff;
            public AddStat(Buff buff)
            {
                this.buff = buff;
            }
            public void UseBuff()
            {
                if (buff.actor != null)
                {
                    buff.actor.AddStat(buff.StatType, buff.Amount, buff.ValueType);
                }
            }
        }
        #endregion

        #region ICancelBuff
        public interface ICancelBuff
        {
            void CancelBuff();
        }
        
        public class NothingCancel : ICancelBuff
        {
            public void CancelBuff() { }
        }
        public class RemoveStat : ICancelBuff
        {
            readonly Buff buff;
            public RemoveStat(Buff buff)
            {
                this.buff = buff;
            }
            public void CancelBuff()
            {
                if (buff.actor != null)
                {
                    buff.actor.AddStat(buff.StatType, -buff.Amount, buff.ValueType);
                }
            }
        }
        #endregion

        #region IBuffCondition

        public interface IBuffCondition
        {
            bool CheckCondition(BuffEvent.EventInfo eventInfo,Buff buff);
        }     
        
        public class IsMelee : IBuffCondition
        {
            public bool CheckCondition(BuffEvent.EventInfo eventInfo, Buff buff)
            {
                if (eventInfo != null)
                {
                    if (eventInfo.obtainedItem != null)
                    {
                        return eventInfo.obtainedItem is MeleeWeapon;
                    }
                }
                return false;
            }
        }
        public class IsRange : IBuffCondition
        {
            public bool CheckCondition(BuffEvent.EventInfo eventInfo, Buff buff)
            {
                if (eventInfo != null)
                {
                    if (eventInfo.obtainedItem != null)
                    {
                        return eventInfo.obtainedItem is RangeWeapon;
                    }
                }
                return false;
            }
        }

        public class IsArmour : IBuffCondition
        {
            public bool CheckCondition(BuffEvent.EventInfo eventInfo, Buff buff)
            {
                if (eventInfo != null)
                {
                    if (eventInfo.obtainedItem != null)
                    {
                        return eventInfo.obtainedItem is Armour;
                    }
                }
                return false;
            }
        }
        public class CheckItemType : IBuffCondition
        {
            public bool CheckCondition(BuffEvent.EventInfo eventInfo, Buff buff)
            {
                if (eventInfo != null)
                {
                    if (eventInfo.obtainedItem != null)
                    {
                        return eventInfo.obtainedItem.Type == buff.ConditionParam;
                    }
                }
                return false;
            }
        }
        #endregion
    }
}