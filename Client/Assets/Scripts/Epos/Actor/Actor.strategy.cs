using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos
{
    public partial class Actor : MonoBehaviour
    {
        public interface IStatStrategy
        {
            float GetFinalStat(ActorStatType type);
        }
        public class BasicStatStrategy : IStatStrategy
        {
            readonly Actor actor;
            public BasicStatStrategy(Actor actor)
            {
                this.actor = actor;
            }

            public float GetFinalStat(ActorStatType type)
            {
                return (actor.BaseStat.stats[type] + actor.BonusStat.Value[type]) * (1 + actor.BonusStat.Ratio[type] / 100);
            }
        }

        public class FixStatStrategy : IStatStrategy
        {
            readonly float value;
            public FixStatStrategy(float value)
            {
                this.value = value;
            }
            public float GetFinalStat(ActorStatType type)
            {
                return value;
            }
        }
    }
}