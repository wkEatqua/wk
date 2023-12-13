using Shared.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Data
{
    public class EposCardEventInfo
    {
        public long Index => data.Index;
        public long BgText => data.BgText;
        public Define.CardPattern Pattern => data.Pattern;
        public long EventName => data.EventName;
        public long EventText => data.EventText;

        readonly EposCardEvent data;
        public EposCardEventInfo(EposCardEvent data) { this.data = data; }
    }

    public class EposBuffInfo
    {
        public long Index => data.Index;
        public string BuffType => data.BuffType;
        public Define.BuffEventType ActiveType => data.ActiveType;
        public float Amount => data.Amount;
        public Define.ActorStatType StatType => data.StatType;
        public Define.ValueType ValueType => data.ValueType;
        public int BuffCondition1 => data.BuffCondition1;
        public int BuffCondition2 => data.BuffCondition2;
        public int ConditionParam => data.ConditionParam;
        readonly EposBuff data;
        public EposBuffInfo(EposBuff data) { this.data = data; }
    }
    public class EposBuffGroupInfo
    {
        public long GroupIndex => data.GroupIndex;
        public long BuffIndex => data.BuffIndex;
        public int Chance => data.Chance;

        readonly EposBuffGroup data;
        public EposBuffGroupInfo(EposBuffGroup data)
        {           
            this.data = data;
        }
    }
    public class BuffData : Database
    {
        static IDictionary<long, EposBuffInfo> buffDict = new Dictionary<long, EposBuffInfo>();
        static IDictionary<long, List<EposBuffInfo>> groupDict = new Dictionary<long, List<EposBuffInfo>>();
        static IDictionary<long, EposCardEventInfo> eventDict = new Dictionary<long, EposCardEventInfo>();

        public override void ProcessDataLoad(string path)
        {
            buffDict = new Data<EposBuff>().GetData(path).ToDictionary(kv => kv.Index, kv => new EposBuffInfo(kv));
            {
                var tempDict = new Data<EposBuffGroup>().GetData(path);
                groupDict.Clear();
                foreach ( var kv in tempDict )
                {
                    if(!groupDict.ContainsKey(kv.GroupIndex))
                    {
                        groupDict.Add(kv.GroupIndex, new());
                    }
                    groupDict[kv.GroupIndex] ??= new();
                    TryGetBuff(kv.BuffIndex, out EposBuffInfo buff);

                    if (buff != null)
                    {
                        groupDict[kv.GroupIndex].Add(buff);
                    }
                }
            }

            eventDict = new Data<EposCardEvent>().GetData(path).ToDictionary(kv => kv.Index,kv => new EposCardEventInfo(kv));
        }

        public static bool TryGetBuff(long Index,out  EposBuffInfo buff)
        {
            return buffDict.TryGetValue(Index, out buff);
        }
        public static bool TryGetBuffGroup(long index, out List<EposBuffInfo> groups)
        {
            return groupDict.TryGetValue(index, out groups);
        }
        public static bool TryGetCardEvent(long index, out EposCardEventInfo cardEvent)
        {
            return eventDict.TryGetValue(index, out cardEvent);
        }
    }
}