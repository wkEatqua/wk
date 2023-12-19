using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shared.Model;

namespace Epos.Buff
{
    public class StatBuff : Buff
    {
        protected override IUseBuff UseStrategy => useStrategy;

        protected override ICancelBuff CancelStrategy => cancelStrategy;

        readonly IUseBuff useStrategy;
        readonly ICancelBuff cancelStrategy;
        public StatBuff() : base()
        {
            useStrategy = new AddStat(this);
            cancelStrategy = new RemoveStat(this);
        }
        protected override void Invoke(BuffEvent.EventInfo info)
        {
            switch(ValueType)
            {
                case AddType.Value:
                    info.stat.AddValue(StatType, Amount);
                    break;
                case AddType.Ratio:
                    info.stat.AddRatio(StatType, Amount);
                    break;
            }
        }
    }
}