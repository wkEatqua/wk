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
    public class EposItemRateInfo
    {
        public long ID => data.ID;
        public long ObjectID => data.ObjectID;
        public int Tier => data.Tier;
        public long Item1 => data.Item1;
        public int ItemRate1 => data.ItemRate1;
        public long Item2 => data.Item2;
        public int ItemRate2 => data.ItemRate2;

        public long Item3 => data.Item3;
        public int ItemRate3 => data.ItemRate3;

        public long Item4 => data.Item4;
        public int ItemRate4 => data.ItemRate4;

        public long Item5 => data.Item5;
        public int ItemRate5 => data.ItemRate5;

        readonly EposItemRate data;
        public EposItemRateInfo(EposItemRate data)
        {
            this.data = data;
        }
    }
    public class ItemData : Database
    {
        static IDictionary<long, EposItemInfo> ItemDict = new Dictionary<long, EposItemInfo>();
        static IDictionary<long, EposItemRateInfo> ItemRateDict = new Dictionary<long, EposItemRateInfo>();

        public override void ProcessDataLoad(string path)
        {
            ItemDict.Clear();
            {
                ItemDict = new Data<EposItem>().GetData(path).ToDictionary(kv => kv.ID, kv => new EposItemInfo(kv));
            }
            ItemRateDict.Clear();
            {
                ItemRateDict = new Data<EposItemRate>().GetData(path).ToDictionary(kv => kv.ID, kv => new EposItemRateInfo(kv));
            }
        }
        public static bool TryGetItemInfo(long ID, out EposItemInfo itemInfo)
        {
            return ItemDict.TryGetValue(ID, out itemInfo);
        }
        public static bool TryGetItemRateInfo(long ID,out  EposItemRateInfo itemRateInfo)
        {
            return ItemRateDict.TryGetValue(ID, out itemRateInfo);
        }
    }
}