using Shared.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Data
{
    public class EposItemInfo
    {
        public ItemType Type => data.Type;
        public string Name => data.Name;
        public long DescText => data.DescText;
        public int BaseStat => data.BaseStat;
        public int UseCount => data.UseCount;
        public int Range => data.Range;
        readonly EposItem data;
        public EposItemInfo(EposItem data)
        {
            this.data = data;
        }
    }
    public class ItemData : Database
    {
        static IDictionary<long, EposItemInfo> ItemDict = new Dictionary<long, EposItemInfo>();

        public override void ProcessDataLoad(string path)
        {

            ItemDict.Clear();
            {
                ItemDict = new Data<EposItem>().GetData(path).ToDictionary(kv => kv.ID, kv => new EposItemInfo(kv));
            }
        }
        public static bool TryGetItemInfo(long ID, out EposItemInfo itemInfo)
        {
            return ItemDict.TryGetValue(ID, out itemInfo);
        }

    }
}