using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Epos
{
    [System.Serializable]
    public class BaseStat
    {
        [SerializeField] float MaxHp; // 최대 체력    
        [SerializeField] float Atk;  // 공격력       
        [SerializeField] float AtkRange; // 공격 사거리
        [SerializeField] float Def; // 방어력       
        [SerializeField] float CritProb; // 크리티컬 확률
        [SerializeField] float CritDmg; // 크리티컬 데미지       
        [SerializeField] float DmgTake = 100; // 받는 피해량

        public Dictionary<ActorStatType, float> stats = new();

        public float maxHp
        {
            get { return stats[ActorStatType.MaxHp]; }
            set
            {
                stats[ActorStatType.MaxHp] = value < 0 ? 0 : value;
            }
        }
        public float atk
        {
            get { return stats[ActorStatType.Atk]; }
            set { stats[ActorStatType.Atk] = value < 0 ? 0 : value; }
        }       
        public float atkRange
        {
            get { return stats[ActorStatType.AtkRange]; }
            set { stats[ActorStatType.AtkRange] = value < 0 ? 0 : value; }
        }
        public float dmgTake
        {
            get { return stats[ActorStatType.DmgTake]; }
            set { stats[ActorStatType.DmgTake] = value; }
        }
        public float def
        {
            get { return stats[ActorStatType.Def]; }
            set { stats[ActorStatType.Def] = value; }
        }       
        public float critProb
        {
            get { return stats[ActorStatType.CritProb]; }
            set { stats[ActorStatType.CritProb] = value < 0 ? 0 : value; }
        }
        public float critDmg
        {
            get { return stats[ActorStatType.CritDmg]; }
            set { stats[ActorStatType.CritDmg] = value < 0 ? 0 : value; }
        }
        
        public void Init()
        {
            var fields = typeof(BaseStat).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            stats.Clear();
            foreach (ActorStatType x in Enum.GetValues(typeof(ActorStatType)))
            {
                stats.TryAdd(x, 0);
            }

            foreach (var field in fields)
            {
                if (!Enum.IsDefined(typeof(ActorStatType), field.Name))
                {
                    continue;
                }

                ActorStatType statType = (ActorStatType)Enum.Parse(typeof(ActorStatType), field.Name);
                if (stats.ContainsKey(statType))
                {
                    stats[statType] += (float)field.GetValue(this);
                }
            }
        }
    }
    public partial class Actor : MonoBehaviour
    {
        [Tooltip("0이 아닌 값으로 설정하면 해당 체력으로 생성(디버그용)")]
        [SerializeField] protected float curHp = 0; // 현재 체력   

        [SerializeField] BaseStat baseStat = new();
        public BaseStat BaseStat => baseStat;

        private readonly BonusStat<ActorStatType> bonusStat = new();

        readonly List<BonusStat<ActorStatType>> bonusStats = new();
        protected virtual BonusStat<ActorStatType> BonusStat
        {
            get
            {               
                BonusStat<ActorStatType> stat = new();

                bonusStats.ForEach(x => stat += x);

                return stat;
            }
        }

        protected IDictionary<ActorStatType, IStatStrategy> statStrategies = new Dictionary<ActorStatType, IStatStrategy>();

        void InitStatStrategy()
        {
            foreach (ActorStatType x in Enum.GetValues(typeof(ActorStatType)))
            {
                statStrategies.Add(x, new BasicStatStrategy(this));
            }
        }
        virtual public float CurHp
        {
            get { return curHp; }
            set
            {               
                if (value < 0)
                {
                    curHp = 0;
                    Die();
                }
                else if (value > BaseStat.maxHp) curHp = BaseStat.maxHp;
                else curHp = value;                
            }
        }
        virtual public float MaxHp
        {
            get
            {
                float value = statStrategies[ActorStatType.MaxHp].GetFinalStat(ActorStatType.MaxHp);
                if (value < 0) return 0;
                return value;
            }
        }

        virtual public float Atk
        {
            get
            {
                float value = statStrategies[ActorStatType.Atk].GetFinalStat(ActorStatType.Atk);
                if (value < 0) return 0;
                return value;
            }
        }
        virtual public float AtkRange
        {
            get
            {
                float value = statStrategies[ActorStatType.AtkRange].GetFinalStat(ActorStatType.AtkRange);
                if (value < 0) return 0;
                return value;
            }
        }
        virtual public float DmgTake
        {
            get
            {
                float value = statStrategies[ActorStatType.DmgTake].GetFinalStat(ActorStatType.DmgTake);
                if (value < 0) return 0;
                return value;
            }
        }
        virtual public float Def
        {
            get
            {
                float value = statStrategies[ActorStatType.Def].GetFinalStat(ActorStatType.Def);

                return value;
            }
        }

        virtual public float CritProb
        {
            get
            {
                float value = statStrategies[ActorStatType.CritProb].GetFinalStat(ActorStatType.CritProb);
                if (value < 0) return 0;
                else if (value > 100) return 100;

                return value;
            }
        }
        virtual public int CritDmg
        {
            get
            {
                float value = statStrategies[ActorStatType.CritDmg].GetFinalStat(ActorStatType.CritDmg);
                if (value < 0) return 0;

                return (int)MathF.Round(value);
            }
        }       

        public void AddStat(ActorStatType statType, float amount, StatType type)
        {
            switch (type)
            {
                case StatType.Value:
                    bonusStat.AddValue(statType, amount);
                    break;
                case StatType.Ratio:
                    bonusStat.AddRatio(statType, amount);
                    break;
            }
        }

        public void ChangeStatStrategy(ActorStatType statType, IStatStrategy strategy)
        {
            statStrategies[statType] = strategy;
        }
    }
}