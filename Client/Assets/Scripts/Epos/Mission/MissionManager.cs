using Shared.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shared.Model;
using System.Linq;
using UnityEngine.Events;

namespace Epos.Mission
{
    public class MissionManager : Singleton<MissionManager>
    {
        MissionInfo info;

        public MissionSubject subject; // 목표
        public MissionReward reward; // 보상
        public MissionPenalty penalty; // 패널티

        public int RemainCount; // 임무 시작까지 남은턴

        public UnityEvent OnMissionStart = new(); // 미션 시작할 때
        public UnityEvent OnMissionGiven = new(); // 미션 주어졌을 때 (시작까지 남은 턴 시작)
        public UnityEvent OnMissionComplete = new(); // 미션 클리어 시
        public UnityEvent OnMissionFail = new(); // 미션 실패시
        bool isMission = false;
        void ResetMission()
        {
            isMission = false;
            MissionData.TryGetMissionInfo(EposManager.Instance.level, out info);
            RemainCount = Random.Range(info.CountValueMin, info.CountValueMax + 1);
            MissionData.TryGetMissionSubjectGroup(info.MissionSubjectID, out List<MissionSubjectInfo> subjectList);
            List<MissionSubjectInfo> sl = new();
            subjectList.ForEach(x =>
            {
                for (int i = 0; i < x.Rate; i++)
                {
                    sl.Add(x);
                }
            });

            int rand = Random.Range(0, sl.Count);
            subject = new(sl[rand]);
            MissionData.TryGetMissionRewardGroup(info.MissionRewardID, out List<MissionRewardInfo> rewardList);
            List<MissionRewardInfo> rl = new();

            rewardList.ForEach(x =>
            {
                for (int i = 0; i < x.Rate; i++)
                {
                    rl.Add(x);
                }
            });

            rand = Random.Range(0, rl.Count);
            reward = new(rl[rand]);

            MissionData.TryGetMissionPenaltyGroup(info.MissionPenaltyID, out List<MissionPenaltyInfo> pl);
            List<MissionPenaltyInfo> penaltyList = new();

            pl.ForEach(x =>
            {
                for (int i = 0; i < x.Rate; i++)
                {
                    penaltyList.Add(x);
                }
            });

            rand = Random.Range(0, penaltyList.Count);
            penalty = new(pl[rand]);

            OnMissionGiven.Invoke();
        }
        private void Start()
        {
            ResetMission();
            EposManager.Instance.OnGameReset.AddListener(ResetMission);
            TurnManager.Instance.OnEnemyTurnEnd += CountMinus;
            TurnManager.Instance.OnPlayerTurnStart += CheckMissionStart;
        }

        IEnumerator CountMinus()
        {
            RemainCount--;
            yield return null;
        }
        IEnumerator CheckMissionStart()
        {
            if (!isMission && RemainCount == 0)
            {
                StartMission();
            }

            yield return null;
        }

        public void StartMission()
        {
            isMission = true;
            subject.StartMission();
            OnMissionStart.Invoke();
        }
        public void Complete()
        {
            isMission = false;
            reward?.GetReward();
            ResetMission();
            OnMissionComplete.Invoke();
        }
        public void Fail()
        {
            penalty?.GetPenalty();
            OnMissionFail.Invoke();
        }
    }
}