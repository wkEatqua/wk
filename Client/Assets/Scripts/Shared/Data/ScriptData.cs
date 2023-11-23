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
    public class ScriptData : Database
    {
        static IDictionary<long,List<CharacterScriptInfo>> charDict = new Dictionary<long,List<CharacterScriptInfo>>();
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
        }

        public static bool TryGetCharacterScript(long groupId, out List<CharacterScriptInfo> list)
        {
            return charDict.TryGetValue(groupId, out list);
        }
    }
}