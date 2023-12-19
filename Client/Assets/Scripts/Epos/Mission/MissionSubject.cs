using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shared.Model;
using Shared.Data;

namespace Epos.Mission
{
    public class MissionSubject
    {
        public MissionSubjectInfo data;
        MissionManager manager => MissionManager.Instance;
        int targetCount;
        int maxTurn;

        int curCount;
        IMissionStrategy condition;
        public MissionSubject(MissionSubjectInfo data)
        {
            this.data = data;
            targetCount = Random.Range(data.SubjectCountMin, data.SubjectCountMax + 1);
            maxTurn = Random.Range(data.SubjectReqMin, data.SubjectReqMax + 1);
        }

        public void StartMission()
        {
            TurnManager.Instance.OnEnemyTurnEnd += CountMinus;
            curCount = 0;
            EposManager.Instance.Player.AddEvent(data.Type, info =>
            {
                if(condition.Check(info))
                {
                    curCount++;
                }
                if(curCount >= targetCount)
                {
                    Complete();
                }
            });
        }

        IEnumerator CountMinus()
        {
            maxTurn--;
            if (maxTurn <= 0) Fail();
            yield return null;
        }

        public void Complete()
        {
            TurnManager.Instance.OnEnemyTurnEnd -= CountMinus;
            MissionManager.Instance.Complete();
        }
        public void Fail()
        {
            TurnManager.Instance.OnEnemyTurnEnd -= CountMinus;
        }
    }
}