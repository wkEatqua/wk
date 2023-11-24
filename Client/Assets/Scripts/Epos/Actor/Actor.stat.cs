using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.SceneTemplate;
using UnityEngine;

namespace Epos
{
    [System.Serializable]
    public class BaseStat
    {
        [SerializeField] int MaxHp; // 최대 체력    
        [SerializeField] int Atk;  // 공격력       
        [SerializeField] int AtkRange; // 공격 사거리
        [SerializeField] int Def; // 방어력       
        [SerializeField] int CritProb; // 크리티컬 확률
        [SerializeField] int CritDmg = 125; // 크리티컬 데미지       
        [SerializeField] int DmgTake = 100; // 받는 피해량
        [SerializeField] int MoveSpeed = 1; // 한 턴에 이동 거리

        public Dictionary<ActorStatType, int> stats = new();

        public int maxHp
        {
            get { return stats[ActorStatType.MaxHp]; }
            set
            {
                stats[ActorStatType.MaxHp] = value < 0 ? 0 : value;
            }
        }
        public int atk
        {
            get { return stats[ActorStatType.Atk]; }
            set { stats[ActorStatType.Atk] = value < 0 ? 0 : value; }
        }       
        public int atkRange
        {
            get { return stats[ActorStatType.AtkRange]; }
            set { stats[ActorStatType.AtkRange] = value < 0 ? 0 : value; }
        }
        public int dmgTake
        {
            get { return stats[ActorStatType.DmgTake]; }
            set { stats[ActorStatType.DmgTake] = value; }
        }
        public int def
        {
            get { return stats[ActorStatType.Def]; }
            set { stats[ActorStatType.Def] = value; }
        }       
        public int critProb
        {
            get { return stats[ActorStatType.CritProb]; }
            set { stats[ActorStatType.CritProb] = value < 0 ? 0 : value; }
        }
        public int critDmg
        {
            get { return stats[ActorStatType.CritDmg]; }
            set { stats[ActorStatType.CritDmg] = value < 0 ? 0 : value; }
        }
        public int moveSpeed
        {
            get { return stats[ActorStatType.MoveSpeed]; }
            set { stats[ActorStatType.MoveSpeed] = value < 0 ? 0 : value; }
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
                    stats[statType] += (int)field.GetValue(this);
                }
            }
        }
    }
    public partial class Actor
    {
        [Tooltip("0이 아닌 값으로 설정하면 해당 체력으로 생성(디버그용)")]
        [SerializeField] protected int curHp = 0; // 현재 체력   

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
        virtual public int CurHp
        {
            get { return curHp; }
            set
            {               
                if (value <= 0)
                {
                    curHp = 0;
                    Die();
                }
                else if (value > MaxHp) curHp = MaxHp;
                else curHp = value;                
            }
        }
        virtual public int MaxHp
        {
            get
            {
                int value = statStrategies[ActorStatType.MaxHp].GetFinalStat(ActorStatType.MaxHp);
                if (value < 0) return 0;
                return value;
            }
        }

        virtual public int Atk
        {
            get
            {
                int value = statStrategies[ActorStatType.Atk].GetFinalStat(ActorStatType.Atk);
                if (value < 0) return 0;
                return value;
            }
        }
        virtual public int AtkRange
        {
            get
            {
                int value = statStrategies[ActorStatType.AtkRange].GetFinalStat(ActorStatType.AtkRange);
                if (value < 0) return 0;
                return value;
            }
        }
        virtual public int DmgTake
        {
            get
            {
                int value = statStrategies[ActorStatType.DmgTake].GetFinalStat(ActorStatType.DmgTake);
                if (value < 0) return 0;
                return value;
            }
        }
        virtual public int Def
        {
            get
            {
                int value = statStrategies[ActorStatType.Def].GetFinalStat(ActorStatType.Def);

                return value;
            }
        }

        virtual public int CritProb
        {
            get
            {
                int value = statStrategies[ActorStatType.CritProb].GetFinalStat(ActorStatType.CritProb);
                if (value < 0)
                    return 0;
                else if (value > 100)
                    return 100;
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

        public virtual int MoveSpeed
        {
            get
            {
                float value = statStrategies[ActorStatType.MoveSpeed].GetFinalStat(ActorStatType.MoveSpeed);
                if (value < 0) return 0;

                return (int)Mathf.Round(value);
            }
        }
        public void AddStat(ActorStatType statType, int amount, StatType type)
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