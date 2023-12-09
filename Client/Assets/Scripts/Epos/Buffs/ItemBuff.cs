using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos.Buff
{
    public class ItemBuff : Buff
    {
        protected override IUseBuff UseStrategy => useStrategy;

        protected override ICancelBuff CancelStrategy => cancelStrategy;

        readonly IUseBuff useStrategy;

        readonly ICancelBuff cancelStrategy;

        public ItemBuff()
        {
            useStrategy = new NothingUse();
            cancelStrategy = new NothingCancel();
        }
        protected override void Invoke(BuffEvent.EventInfo info)
        {
            Item item = info.obtainedItem;

            item?.AddStat(StatType, AddType, Amount);
        }
    }
}