using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos
{
    public partial class Actor : MonoBehaviour
    {
        public interface IStatStrategy
        {
            int GetFinalStat(ActorStatType type);
        }
        public class BasicStatStrategy : IStatStrategy
        {
            readonly Actor actor;
            public BasicStatStrategy(Actor actor)
            {
                this.actor = actor;
            }

            public int GetFinalStat(ActorStatType type)
            {
                return (int)((actor.BaseStat.stats[type] + actor.BonusStat.Value[type]) * (1 + actor.BonusStat.Ratio[type] / 100f));
            }
        }

        public class FixStatStrategy : IStatStrategy
        {
            readonly int value;
            public FixStatStrategy(int value)
            {
                this.value = value;
            }
            public int GetFinalStat(ActorStatType type)
            {
                return value;
            }
        }
    }
}