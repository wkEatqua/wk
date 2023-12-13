using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos.Buff
{
    public partial class Buff 
    {
        public interface IUseBuff
        {
            void UseBuff();
        }
        public interface ICancelBuff
        {
            void CancelBuff();
        }      

        public class NothingUse : IUseBuff
        {
            public void UseBuff()
            {
            }
        }
        public class NothingCancel : ICancelBuff
        {
            public void CancelBuff() { }
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
    }
}