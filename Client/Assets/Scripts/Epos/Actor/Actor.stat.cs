using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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

        public Dictionary<Define.ActorStatType, int> stats = new();

        public int maxHp
        {
            get { return stats[Define.ActorStatType.MaxHp]; }
            set
            {
                stats[Define.ActorStatType.MaxHp] = value < 0 ? 0 : value;
            }
        }
        public int atk
        {
            get { return stats[Define.ActorStatType.Atk]; }
            set { stats[Define.ActorStatType.Atk] = value < 0 ? 0 : value; }
        }       
        public int atkRange
        {
            get { return stats[Define.ActorStatType.AtkRange]; }
            set { stats[Define.ActorStatType.AtkRange] = value < 0 ? 0 : value; }
        }
        public int dmgTake
        {
            get { return stats[Define.ActorStatType.DmgTake]; }
            set { stats[Define.ActorStatType.DmgTake] = value; }
        }
        public int def
        {
            get { return stats[Define.ActorStatType.Def]; }
            set { stats[Define.ActorStatType.Def] = value; }
        }       
        public int critProb
        {
            get { return stats[Define.ActorStatType.CritProb]; }
            set { stats[Define.ActorStatType.CritProb] = value < 0 ? 0 : value; }
        }
        public int critDmg
        {
            get { return stats[Define.ActorStatType.CritDmg]; }
            set { stats[Define.ActorStatType.CritDmg] = value < 0 ? 0 : value; }
        }
        public int moveSpeed
        {
            get { return stats[Define.ActorStatType.MoveSpeed]; }
            set { stats[Define.ActorStatType.MoveSpeed] = value < 0 ? 0 : value; }
        }

        public void Init()
        {
            var fields = typeof(BaseStat).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            stats.Clear();
            foreach (Define.ActorStatType x in Enum.GetValues(typeof(Define.ActorStatType)))
            {
                stats.TryAdd(x, 0);
            }

            foreach (var field in fields)
            {
                if (!Enum.IsDefined(typeof(Define.ActorStatType), field.Name))
                {
                    continue;
                }
                
                Define.ActorStatType statType = (Define.ActorStatType)Enum.Parse(typeof(Define.ActorStatType), field.Name);
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

        protected BonusStat<Define.ActorStatType> bonusStat = new();

        readonly List<BonusStat<Define.ActorStatType>> bonusStats = new();

        protected virtual BonusStat<Define.ActorStatType> BonusStat
        {
            get
            {               
                BonusStat<Define.ActorStatType> stat = new();
                stat += bonusStat;
                bonusStats.ForEach(x => stat += x);

                return stat;
            }
        }

        protected IDictionary<Define.ActorStatType, IStatStrategy> statStrategies = new Dictionary<Define.ActorStatType, IStatStrategy>();

        void InitStatStrategy()
        {
            foreach (Define.ActorStatType x in Enum.GetValues(typeof(Define.ActorStatType)))
            {
                statStrategies.Add(x, new BasicStatStrategy(this));
            }
        }
        virtual public int CurHp
        {
            get { return curHp; }
            set
            {
                if (value < curHp)
                {
                    float dmg = curHp - value;
                    DmgTextShow.ShowDmg(transform.position, Color.yellow, dmg);
                }
                else
                {
                    float dmg = value - curHp;
                    DmgTextShow.ShowDmg(transform.position, Color.green, dmg);
                }

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
                int value = statStrategies[Define.ActorStatType.MaxHp].GetFinalStat(Define.ActorStatType.MaxHp);
                if (value < 0) return 0;
                return value;
            }
        }

        virtual public int Atk
        {
            get
            {
                int value = statStrategies[Define.ActorStatType.Atk].GetFinalStat(Define.ActorStatType.Atk);
                if (value < 0) return 0;
                return value;
            }
        }
        virtual public int AtkRange
        {
            get
            {
                int value = statStrategies[Define.ActorStatType.AtkRange].GetFinalStat(Define.ActorStatType.AtkRange);
                if (value < 0) return 0;
                return value;
            }
        }
        virtual public int DmgTake
        {
            get
            {
                int value = statStrategies[Define.ActorStatType.DmgTake].GetFinalStat(Define.ActorStatType.DmgTake);
                if (value < 0) return 0;
                return value;
            }
        }
        virtual public int Def
        {
            get
            {
                int value = statStrategies[Define.ActorStatType.Def].GetFinalStat(Define.ActorStatType.Def);

                return value;
            }
        }

        virtual public int CritProb
        {
            get
            {
                int value = statStrategies[Define.ActorStatType.CritProb].GetFinalStat(Define.ActorStatType.CritProb);
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
                float value = statStrategies[Define.ActorStatType.CritDmg].GetFinalStat(Define.ActorStatType.CritDmg);
                if (value < 0) return 0;

                return (int)MathF.Round(value);
            }
        }       

        public virtual int MoveSpeed
        {
            get
            {
                float value = statStrategies[Define.ActorStatType.MoveSpeed].GetFinalStat(Define.ActorStatType.MoveSpeed);
                if (value < 0) return 0;

                return (int)Mathf.Round(value);
            }
        }

        public virtual int Damage
        {
            get
            {
                float value = statStrategies[Define.ActorStatType.Damage].GetFinalStat(Define.ActorStatType.Damage);
                if (value < 0) return 0;

                return (int)Mathf.Round(value);
            }
        }

        public virtual int GoldGain
        {
            get
            {
                float value = statStrategies[Define.ActorStatType.GoldGain].GetFinalStat(Define.ActorStatType.GoldGain);
                if (value < 0) return 0;

                return (int)MathF.Round(value);
            }
        }

        public void AddStat(Define.ActorStatType statType, int amount, Define.ValueType type)
        {
            switch (type)
            {
                case Define.ValueType.Value:
                    bonusStat.AddValue(statType, amount);
                    break;
                case Define.ValueType.Ratio:
                    bonusStat.AddRatio(statType, amount);
                    break;
            }
        }

        public void ChangeStatStrategy(Define.ActorStatType statType, IStatStrategy strategy)
        {
            statStrategies[statType] = strategy;
        }
    }
}