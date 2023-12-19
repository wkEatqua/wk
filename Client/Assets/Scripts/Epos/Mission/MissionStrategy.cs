using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos.Mission
{
    public interface IMissionStrategy
    {
        public bool Check(BuffEvent.EventInfo info);
    }


}