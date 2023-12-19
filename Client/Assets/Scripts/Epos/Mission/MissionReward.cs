using Shared.Data;
using Shared.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos.Mission
{
    public class MissionReward
    {
        readonly MissionRewardInfo info;

        public MissionReward(MissionRewardInfo info)
        {
            this.info = info;
        }

        public void GetReward()
        {
            switch(info.RewardType)
            {
                case RewardType.Item:
                    Item item = ItemFactory.Create(info.RewardID);
                    item.OnCollect();
                    break;
                case RewardType.Stat:
                    int rand = Random.Range(info.RewardValueMin, info.RewardValueMax + 1);
                    EposManager.Instance.Player.AddStat((ActorStatType)info.RewardDetail, rand, info.AddType);
                    break;
            }
        }
    }
}