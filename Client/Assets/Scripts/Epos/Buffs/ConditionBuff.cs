using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos.Buff
{
    public class ConditionBuff : Buff
    {
        protected override IUseBuff UseStrategy => new NothingUse();

        protected override ICancelBuff CancelStrategy => new NothingCancel();

        protected override void Invoke(BuffEvent.EventInfo info)
        {
            throw new System.NotImplementedException();
        }
    }
}