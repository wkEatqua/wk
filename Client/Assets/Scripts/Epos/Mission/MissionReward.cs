using Shared.Data;
using Shared.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos.Mission
{
    public class MissionReward
    {
        public readonly MissionRewardInfo info;
        public readonly int value;
        public MissionReward(MissionRewardInfo info)
        {
            this.info = info;
            value = Random.Range(info.RewardValueMin, info.RewardValueMax + 1);
        }

        public void GetReward()
        {
            switch(info.RewardType)
            {
                case RewardType.Item:
                    Item item = ItemFactory.Create(info.RewardID);
                    item.OnMove();
                    break;
                case RewardType.Stat:
                    EposManager.Instance.Player.AddStat((ActorStatType)info.RewardDetail, value, info.AddType);
                    break;
            }
        }
    }
}