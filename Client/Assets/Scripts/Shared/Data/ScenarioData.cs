using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Org.BouncyCastle.Crypto.Generators;
using Shared.Model;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;

namespace Shared.Data
{
    public class ScenarioWorldInfo
    {
        public long UniqueId => data.UniqueId;
        public string LocalizeKey => data.LocalizeKey;

        public string Name => data.Name;
        readonly ScenarioWorld data;

        public ScenarioWorldInfo(ScenarioWorld data) { this.data = data; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"UniqueId : {UniqueId}");
            sb.AppendLine($"LocalizeKey : {LocalizeKey}");
            return sb.ToString();
        }
    }
    public class ScenarioPageImageInfo
    {
        public long PageId => data.PageId;
        public string ImagePath => data.ImagePath;
        public int ImageActiveOrder => data.ImageActiveOrder;

        readonly ScenarioPageImage data;
        public ScenarioPageImageInfo(ScenarioPageImage data) { this.data = data; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"GroupId : {PageId}");
            sb.AppendLine($"ImagePath : {ImagePath}");
            sb.AppendLine($"ImageActiveOrder : {ImageActiveOrder}");
            return sb.ToString();
        }
    }
    public class ScenarioChapterInfo
    {
        public long WorldId => data.WorldId;
        public string Name => data.Name;
        public long UniqueId => data.UniqueId;

        public int VerisimilitudeMin => data.VerisimilitudeMin;
        public int VerisimilitudeMax => data.VerisimilitudeMax;
        public long HeroId => data.HeroId;

        public int DefaultHealthMax => data.DefaultHealthMax;
        public int DefaultEnergyMax => data.DefaultEnergyMax;
        public long OpenConditionId => data.OpenConditionId;
        public long StarRewardId => data.StarRewardId;

        readonly Model.ScenarioChapter data;

        public ScenarioChapterInfo(Model.ScenarioChapter data)
        {
            this.data = data;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"WorldId : {WorldId}");
            sb.AppendLine($"UniqueId : {UniqueId}");

            sb.AppendLine($"VerisimilitudeMin : {VerisimilitudeMin}");
            sb.AppendLine($"VerisimilitudeMax : {VerisimilitudeMax}");
            sb.AppendLine($"ChacracterId : {HeroId}");
            sb.AppendLine($"OpenConditionId : {OpenConditionId}");
            sb.AppendLine($"StarRewardId : {StarRewardId}");
            return sb.ToString();
        }
    }
    public class ScenarioPageInfo
    {
        public int ChapterId => data.ChapterId;
        public long UniqueId => data.UniqueId;
        public string DevName => data.DevName;
        public long SelectGroupId => data.SelectGroupId;

        readonly ScenarioPage data;

        public ScenarioPageInfo(ScenarioPage data) { this.data = data; }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"ChapterId : {ChapterId}");
            sb.AppendLine($"UniqueId : {UniqueId}");
            sb.AppendLine($"DevName : {DevName}");
            sb.AppendLine($"SelectGroupId : {SelectGroupId}");
            return sb.ToString();
        }
    }
    public class ScenarioPageTextInfo
    {
        public long PageId => data.PageId;
        public int Order => data.Order;
        public string Text => data.Text;

        readonly ScenarioPageText data;

        public ScenarioPageTextInfo(ScenarioPageText data) { this.data = data; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"PageId : {PageId}");
            sb.AppendLine($"Order : {Order}");
            sb.AppendLine($"Text : {Text}");
            return sb.ToString();
        }
    }

    public class ScenarioSelectInfo
    {
        public long GroupId => data.GroupId;
        public string SelectText => data.SelectText;
        public SelectType SelectType => data.SelectType;
        public int SelectValue => data.SelectValue;
        public int SelectEnergy => data.SelectEnergy;
        public int SelectVerisimilitude => data.SelectVerisimilitude;

        public bool isDiced;
        readonly ScenarioSelect data;
        public ScenarioSelectInfo(ScenarioSelect data) { this.data = data; isDiced = false; }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"GroupId : {GroupId}");
            sb.AppendLine($"SelectText : {SelectText}");
            sb.AppendLine($"SelectType : {SelectType}");
            sb.AppendLine($"SelectValue : {SelectValue}");
            sb.AppendLine($"SelectVerisimilitude : {SelectVerisimilitude}");
            return sb.ToString();
        }
    }

    public class ScenarioDiceInfo
    {
        public long ChapterId => data.ChapterId;
        public int DiceProbSum => data.DiceProbSum;

        public readonly int?[] DiceProb;
        readonly ScenarioDice data;

        public ScenarioDiceInfo(ScenarioDice data)
        {
            this.data = data;
            DiceProb = typeof(ScenarioDice).GetProperties()
            .Where(p => p.PropertyType == typeof(int?) && p.Name.StartsWith("DiceProb"))
            .Select(p => (int?)p.GetValue(data))
            .ToArray();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"ChapterId : {ChapterId}");
            sb.AppendLine($"DiceProbSum : {DiceProbSum}");
            int count = 0;
            foreach (var item in DiceProb)
            {
                sb.AppendLine($"DiceProb[{count++}] : {item}");
            }
            return sb.ToString();
        }
    }

    public class ScenarioChapterRewardInfo
    {
        public long UniqueId => data.UniqueId;
        public ParcelType RewardType => data.RewardType;
        public long RewardId => data.RewardId;
        public int RewardAmount => data.RewardAmount;

        readonly ScenarioChapterReward data;

        public ScenarioChapterRewardInfo(ScenarioChapterReward data)
        {
            this.data = data;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"UniqueId : {UniqueId}");
            sb.AppendLine($"RewardType : {RewardType}");
            sb.AppendLine($"RewardId : {RewardId}");
            sb.AppendLine($"RewardAmount : {RewardAmount}");
            return sb.ToString();
        }
    }

    public class ScenarioEndingInfo
    {
        public long UniqueId => data.UniqueId;
        public long ChapterId => data.ChapterId;
        public string ImagePath => data.ImagePath;
        public string Name => data.Name;

        readonly ScenarioEnding data;

        public ScenarioEndingInfo(ScenarioEnding data)
        {
            this.data = data;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"UniqueId : {UniqueId}");
            sb.AppendLine($"ChapterId : {ChapterId}");
            sb.AppendLine($"ImagePath : {ImagePath}");
            sb.AppendLine($"Name : {Name}");
            return sb.ToString();
        }
    }


    public class ScenarioData : Database
    {
        static IDictionary<long, ScenarioWorldInfo> WorldDict = new Dictionary<long, ScenarioWorldInfo>();
        static IDictionary<long, ScenarioChapterInfo> ChapterDict = new Dictionary<long, ScenarioChapterInfo>();
        static IDictionary<long, ScenarioPageInfo> PageDict = new Dictionary<long, ScenarioPageInfo>();
        static IDictionary<long, List<ScenarioPageInfo>> PageGroupDict = new Dictionary<long, List<ScenarioPageInfo>>();
        static IDictionary<long, List<ScenarioPageTextInfo>> PageTextDict = new Dictionary<long, List<ScenarioPageTextInfo>>();
        static readonly IDictionary<long, List<ScenarioSelectInfo>> SelectGroupDict = new Dictionary<long, List<ScenarioSelectInfo>>();
        static IDictionary<long, ScenarioDiceInfo> DiceDict = new Dictionary<long, ScenarioDiceInfo>();
        static IDictionary<long, ScenarioChapterRewardInfo> RewardDict = new Dictionary<long, ScenarioChapterRewardInfo>();
        static IDictionary<long, ScenarioEndingInfo> EndingDict = new Dictionary<long, ScenarioEndingInfo>();
        static IDictionary<long, List<ScenarioPageImageInfo>> ImageDict = new Dictionary<long, List<ScenarioPageImageInfo>>();
        public override void ProcessDataLoad(string path)
        {
            WorldDict.Clear();
            {
                var worlds = new Data<ScenarioWorld>().GetData(path);

                WorldDict = worlds.ToDictionary(kv => kv.UniqueId, kv => new ScenarioWorldInfo(kv));
            }

            ChapterDict.Clear();
            {
                var chapters = new Data<ScenarioChapter>().GetData(path);

                ChapterDict = chapters.ToDictionary(kv => kv.UniqueId, kv => new ScenarioChapterInfo(kv));
            }

            PageDict.Clear();
            {
                var pages = new Data<ScenarioPage>().GetData(path);

                PageDict = pages.ToDictionary(kv => kv.UniqueId, kv => new ScenarioPageInfo(kv));
            }
            PageGroupDict.Clear();
            {
                var group = new Data<ScenarioPage>().GetData(path);

                foreach (var x in group)
                {
                    if (!PageGroupDict.ContainsKey(x.ChapterId))
                    {
                        PageGroupDict.Add(x.ChapterId, new List<ScenarioPageInfo>());
                    }

                    if (PageGroupDict[x.ChapterId] == null)
                    {
                        PageGroupDict[x.ChapterId] = new List<ScenarioPageInfo>();
                    }
                    PageGroupDict[x.ChapterId].Add(new ScenarioPageInfo(x));
                }
            }

            PageTextDict.Clear();
            {
                var texts = new Data<ScenarioPageText>().GetData(path);

                foreach (var x in texts)
                {
                    if (!PageTextDict.ContainsKey(x.PageId))
                    {
                        PageTextDict.Add(x.PageId, new List<ScenarioPageTextInfo>());
                    }

                    if (PageTextDict[x.PageId] == null)
                    {
                        PageTextDict[x.PageId] = new List<ScenarioPageTextInfo>();
                    }
                    PageTextDict[x.PageId].Add(new ScenarioPageTextInfo(x));
                }
            }

            SelectGroupDict.Clear();
            {
                var groups = new Data<ScenarioSelect>().GetData(path);

                foreach (var group in groups)
                {
                    if (!SelectGroupDict.ContainsKey(group.GroupId))
                    {
                        SelectGroupDict.Add(group.GroupId, new List<ScenarioSelectInfo>());
                    }

                    if (SelectGroupDict[group.GroupId] == null)
                    {
                        SelectGroupDict[group.GroupId] = new List<ScenarioSelectInfo>();
                    }

                    SelectGroupDict[group.GroupId].Add(new ScenarioSelectInfo(group));
                }
            }
            DiceDict.Clear();
            {
                var dice = new Data<ScenarioDice>().GetData(path);

                DiceDict = dice.ToDictionary(kv => kv.ChapterId, kv => new ScenarioDiceInfo(kv));
            }
            RewardDict.Clear();
            {
                var rewards = new Data<ScenarioChapterReward>().GetData(path);

                RewardDict = rewards.ToDictionary(kv => kv.UniqueId, kv => new ScenarioChapterRewardInfo(kv));
            }
            EndingDict.Clear();
            {
                var ending = new Data<ScenarioEnding>().GetData(path);

                EndingDict = ending.ToDictionary(kv => kv.UniqueId, kv => new ScenarioEndingInfo(kv));
            }
            ImageDict.Clear();
            {
                var groups = new Data<ScenarioPageImage>().GetData(path);

                foreach (var group in groups)
                {
                    if (!ImageDict.ContainsKey(group.PageId))
                    {
                        ImageDict.Add(group.PageId, new());
                    }

                    ImageDict[group.PageId] ??= new();

                    ImageDict[group.PageId].Add(new ScenarioPageImageInfo(group));
                }
            }
        }

        public static bool TryGetWorld(long uniqueId, out ScenarioWorldInfo info)
        {
            return WorldDict.TryGetValue(uniqueId, out info);
        }

        public static bool TryGetChapter(long uniqueId, out ScenarioChapterInfo info)
        {
            return ChapterDict.TryGetValue(uniqueId, out info);
        }
        public static bool TryGetPage(long uniqueId, out ScenarioPageInfo info)
        {
            return PageDict.TryGetValue(uniqueId, out info);
        }
        public static bool TryGetPageGroup(long chapterId, out List<ScenarioPageInfo> info)
        {
            return PageGroupDict.TryGetValue(chapterId, out info);
        }
        public static bool TryGetPageText(long pageId, out List<ScenarioPageTextInfo> info)
        {
            return PageTextDict.TryGetValue(pageId, out info);
        }

        public static bool TryGetSelectGroup(long groupId, out List<ScenarioSelectInfo> infos)
        {
            return SelectGroupDict.TryGetValue(groupId, out infos);
        }

        public static bool TryGetDice(long chapterId, out ScenarioDiceInfo info)
        {
            return DiceDict.TryGetValue(chapterId, out info);
        }

        public static bool TryGetChapterReward(long uniqueId, out ScenarioChapterRewardInfo info)
        {
            return RewardDict.TryGetValue(uniqueId, out info);
        }

        public static bool TryGetEnding(long uniqueId, out ScenarioEndingInfo info)
        {
            return EndingDict.TryGetValue(uniqueId, out info);
        }
        public static bool TryGetPageImages(long groupId, out List<ScenarioPageImageInfo> info)
        {
            return ImageDict.TryGetValue(groupId, out info);
        }
    }
}
