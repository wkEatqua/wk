using Shared.Data;
using Shared.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos.Mission
{
    public class MissionPenalty
    {
        public readonly MissionPenaltyInfo info;
        public readonly int value;
        public MissionPenalty(MissionPenaltyInfo info)
        {
            this.info = info;
            value = Random.Range(info.PenaltyValueMin, info.PenaltyValueMax + 1);
        }

        public void GetPenalty()
        {
            switch(info.Type)
            {
                case PenaltyType.Stat:
                    EposManager.Instance.Player.AddStat((ActorStatType)info.PenaltyType, -value, info.AddType);
                    break;
                case PenaltyType.HP:
                    switch(info.AddType)
                    {
                        case AddType.Value:
                            EposManager.Instance.Player.CurHp -= value;
                            break;
                        case AddType.Ratio:
                            float ratio = (100 - value) / 100;
                            EposManager.Instance.Player.CurHp = (int)(EposManager.Instance.Player.CurHp * ratio);
                            break;
                    }
                    break;
            }
        }
    }
}