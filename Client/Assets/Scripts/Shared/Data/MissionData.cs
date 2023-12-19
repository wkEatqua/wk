using Shared.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shared.Data
{
    public class MissionInfo
    {
        public long ID => data.ID;
        public int Lv => data.Lv;
        public int CountValueMin => data.CountValueMin;
        public int CountValueMax => data.CountValueMax;
        public long MissionSubjectID => data.MissionSubjectID;
        public long MissionRewardID => data.MissionRewardID;
        public long MissionPenaltyID => data.MissionPenaltyID;

        readonly Mission data;
        public MissionInfo(Mission data)
        {
            this.data = data;
        }
    }
    public class MissionSubjectInfo
    {
        public long GroupID => data.GroupID;
        public int Lv => data.Lv;
        public int Rate => data.Rate;
        public EventTypes Type => data.Type;
        public int SubjectCountMin => data.SubjectCountMin;
        public int SubjectCountMax => data.SubjectCountMax;
        public int SubjectReqMin => data.SubjectReqMin;
        public int SubjectReqMax => data.SubjectReqMax;

        readonly MissionSubject data;
        public MissionSubjectInfo(MissionSubject data)
        {
            this.data = data;
        }
    }
    public class MissionRewardInfo
    {
        public long GroupID => data.GroupID;
        public int Lv => data.Lv;
        public int Rate => data.Rate;
        public RewardType RewardType => data.Type;
        public int RewardDetail => data.RewardType;
        public long RewardID => data.GroupID;
        public int RewardValueMin => data.RewardValueMin;
        public int RewardValueMax => data.RewardValueMax;
        public AddType AddType => data.AddType;
        readonly MissionReward data;
        public MissionRewardInfo(MissionReward data)
        {
            this.data = data;
        }
    }
    public class MissionPenaltyInfo
    {
        public long GroupID => data.GroupID;
        public int Lv => data.Lv;
        public int Rate => data.Rate;
        public PenaltyType Type => data.Type;
        public int PenaltyType => data.PenaltyType;
        public int PenaltyValueMin => data.PenaltyValueMin;
        public int PenaltyValueMax => data.PenaltyValueMax;
        public AddType AddType => data.AddType;
        readonly MissionPenalty data;
        public MissionPenaltyInfo(MissionPenalty data)
        {
            this.data = data;
        }
    }
    public class MissionData : Database
    {
        static IDictionary<int,MissionInfo> missionDict = new Dictionary<int, MissionInfo>();
        readonly static IDictionary<long,List<MissionSubjectInfo>> subjectGroup = new Dictionary<long, List<MissionSubjectInfo>>();
        readonly static IDictionary<long,List<MissionRewardInfo>> rewardGroup = new Dictionary<long, List<MissionRewardInfo>>();
        readonly static IDictionary<long,List<MissionPenaltyInfo>> penaltyGroup = new Dictionary<long,List<MissionPenaltyInfo>>();
        public override void ProcessDataLoad(string path)
        {
            missionDict = new Data<Mission>().GetData(path).ToDictionary(kv => kv.Lv, kv => new MissionInfo(kv));
            {
                var group = new Data<MissionSubject>().GetData(path);
                subjectGroup.Clear();
                foreach (var item in group)
                {
                    if (!subjectGroup.ContainsKey(item.GroupID))
                    {
                        subjectGroup.Add(item.GroupID, new());
                    }
                    subjectGroup[item.GroupID].Add(new(item));
                }
            }
            {
                var group = new Data<MissionReward>().GetData(path);
                rewardGroup.Clear();
                foreach (var item in group)
                {
                    if (!rewardGroup.ContainsKey(item.GroupID))
                    {
                        rewardGroup.Add(item.GroupID, new());
                    }
                    rewardGroup[item.GroupID].Add(new(item));
                }
            }
            {
                var group = new Data<MissionPenalty>().GetData(path);
                penaltyGroup.Clear();
                foreach (var item in group)
                {
                    if (!penaltyGroup.ContainsKey(item.GroupID))
                    {
                        penaltyGroup.Add(item.GroupID, new());
                    }
                    penaltyGroup[item.GroupID].Add(new(item));
                }
            }
        }

        public static bool TryGetMissionInfo(int lvl, out MissionInfo info)
        {
            return missionDict.TryGetValue(lvl, out info);
        }
        public static bool TryGetMissionSubjectGroup(long id,out List<MissionSubjectInfo> list)
        {
            return subjectGroup.TryGetValue(id, out list);
        }
        public static bool TryGetMissionRewardGroup(long id,out List<MissionRewardInfo> list)
        {
            return rewardGroup.TryGetValue(id,out list);
        }
        public static bool TryGetMissionPenaltyGroup(long id, out List<MissionPenaltyInfo> list)
        {
            return penaltyGroup.TryGetValue(id, out list);
        }

    }
}