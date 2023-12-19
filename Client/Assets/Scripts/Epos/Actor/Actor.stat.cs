using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Shared.Model;
using System.Linq;
using UnityEngine.Events;

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
    public delegate BonusStat<ActorStatType> StatEvent();

    public partial class Actor
    {
        [Tooltip("0이 아닌 값으로 설정하면 해당 체력으로 생성(디버그용)")]
        [SerializeField] protected int curHp = 0; // 현재 체력   

        [SerializeField] BaseStat baseStat = new();
        public BaseStat BaseStat => baseStat;

        protected BonusStat<ActorStatType> bonusStat = new();

        event StatEvent bonusStatEvent;
        public event StatEvent BonusStatEvent
        {
            add
            {
                bonusStatEvent -= value;
                bonusStatEvent += value;
            }
            remove
            {
                bonusStatEvent -= value;
            }
        }
        protected virtual BonusStat<ActorStatType> BonusStats
        {
            get
            {
                bonusStat ??= new();
                BonusStat<ActorStatType> bs = new BonusStat<ActorStatType>();
                bs += bonusStat;
                if (bonusStatEvent != null)
                {
                    foreach (StatEvent ev in bonusStatEvent.GetInvocationList().Cast<StatEvent>())
                    {
                        bs += ev();
                    }
                }
                return bs;
            }
        }
        public int BonusStat(ActorStatType type)
        {
            int value = statStrategies[type].GetFinalStat(type);


            return value - BaseStat.stats[type];
        }
        protected IDictionary<ActorStatType, IStatStrategy> statStrategies = new Dictionary<ActorStatType, IStatStrategy>();

        [HideInInspector]public UnityEvent OnStatChange = new();
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

        public virtual int Damage
        {
            get
            {
                float value = statStrategies[ActorStatType.Damage].GetFinalStat(ActorStatType.Damage);
                if (value < 0) return 0;

                return (int)Mathf.Round(value);
            }
        }

        public virtual int GoldGain
        {
            get
            {
                float value = statStrategies[ActorStatType.GoldGain].GetFinalStat(ActorStatType.GoldGain);
                if (value < 0) return 0;

                return (int)MathF.Round(value);
            }
        }

        public void AddStat(ActorStatType statType, int amount, AddType type)
        {
            switch (type)
            {
                case AddType.Value:
                    bonusStat.AddValue(statType, amount);
                    break;
                case AddType.Ratio:
                    bonusStat.AddRatio(statType, amount);
                    break;
            }
            OnStatChange.Invoke();
        }

        public void ChangeStatStrategy(ActorStatType statType, IStatStrategy strategy)
        {
            statStrategies[statType] = strategy;
            OnStatChange.Invoke();
        }
    }
}