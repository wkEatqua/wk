using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shared.Model;

namespace Epos
{
    public partial class Actor
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
                return (int)((actor.BaseStat.stats[type] + actor.BonusStats.Value[type]) * (1 + actor.BonusStats.Ratio[type] / 100f));
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