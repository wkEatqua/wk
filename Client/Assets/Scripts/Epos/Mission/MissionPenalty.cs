using Shared.Data;
using Shared.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos.Mission
{
    public class MissionPenalty
    {
        readonly MissionPenaltyInfo info;

        public MissionPenalty(MissionPenaltyInfo info)
        {
            this.info = info;
        }

        public void GetPenalty()
        {
            int rand = Random.Range(info.PenaltyValueMin, info.PenaltyValueMax + 1);
            switch(info.Type)
            {
                case PenaltyType.Stat:
                    EposManager.Instance.Player.AddStat((ActorStatType)info.PenaltyType, -rand, info.AddType);
                    break;
                case PenaltyType.HP:
                    switch(info.AddType)
                    {
                        case AddType.Value:
                            EposManager.Instance.Player.CurHp -= rand;
                            break;
                        case AddType.Ratio:
                            float ratio = (100 - rand) / 100;
                            EposManager.Instance.Player.CurHp = (int)(EposManager.Instance.Player.CurHp * ratio);
                            break;
                    }
                    break;
            }
        }
    }
}