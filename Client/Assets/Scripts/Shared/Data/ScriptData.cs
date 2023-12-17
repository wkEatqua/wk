using Shared.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Data
{
    public class CharacterScriptInfo
    {
        public long GroupId => data.GroupId;
        public string ScriptText => data.ScriptText;
        public long UniqueId => data.UniqueId;

        readonly CharacterScript data;

        public CharacterScriptInfo(CharacterScript data) { this.data = data; }
    }
    public class EposGameTextInfo
    {
        public long ID => data.ID;
        public string Kor => data.Kor;

        readonly EposGameText data;
        public EposGameTextInfo(EposGameText data)
        {
            this.data = data;
        }
    }
    public class ScriptData : Database
    {
        static IDictionary<long,List<CharacterScriptInfo>> charDict = new Dictionary<long,List<CharacterScriptInfo>>();
        static IDictionary<long,EposGameTextInfo> textDict = new Dictionary<long,EposGameTextInfo>();
        public override void ProcessDataLoad(string path)
        {
            var group = new Data<CharacterScript>().GetData(path);

            foreach (var x in group)
            {
                if (!charDict.ContainsKey(x.GroupId))
                {
                    charDict.Add(x.GroupId, new());
                }

                charDict[x.GroupId] ??= new();

                charDict[x.GroupId].Add(new CharacterScriptInfo(x));
            }

            textDict = new Data<EposGameText>().GetData(path).ToDictionary(kv => kv.ID,kv => new EposGameTextInfo(kv));          
        }

        public static bool TryGetCharacterScript(long groupId, out List<CharacterScriptInfo> list)
        {
            return charDict.TryGetValue(groupId, out list);
        }
        public static bool TryGetGameText(long ID, out EposGameTextInfo info)
        {
            return textDict.TryGetValue(ID, out info);
        }
    }
}