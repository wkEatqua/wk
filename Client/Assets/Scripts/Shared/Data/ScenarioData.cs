using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using Shared.Model;

namespace Shared.Data
{
    public class ScenarioWorldInfo
    {
        public long UniqueId
        {
            get
            {
                return data.UniqueId;
            }
        }
        public string LocalizeKey => data.LocalizeKey;
        public string Name => data.Name;
        readonly ScenarioWorld data;

        public ScenarioWorldInfo(ScenarioWorld data) 
        { this.data = data; }       
    }
    public class ScenarioPageImageInfo
    {
        public long UniqueId => data.UniqueId;
        public long GroupId => data.GroupId;
        public string ImagePath => data.ImagePath;
        public int ImageActiveOrder => data.ImageActiveOrder;

        readonly ScenarioPageImage data;
        public ScenarioPageImageInfo(ScenarioPageImage data) { this.data = data; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"GroupId : {GroupId}");
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

        readonly ScenarioChapter data;

        public ScenarioChapterInfo(ScenarioChapter data)
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

        public long TextContentId => data.TextContentId;        
        public long ResultContentGroupId => data.ResultContentGroupId;

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
        public long UniqueId => data.UniqueId;
        public long GroupId => data.GroupId;
        public int Order => data.Order;
        public string Text => data.Text;

        readonly ScenarioPageText data;

        public ScenarioPageTextInfo(ScenarioPageText data) { this.data = data; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"PageId : {GroupId}");
            sb.AppendLine($"Order : {Order}");
            sb.AppendLine($"Text : {Text}");
            return sb.ToString();
        }
    }
    public class ScenarioTextContentInfo
    {
        public long GroupId => data.GroupId;
        public long UniqueId => data.UniqueId;
        public long TextGroupId => data.TextGroupId;
        public long ImageGroupId => data.ImageGroupId;

        readonly ScenarioTextContent data;

        public ScenarioTextContentInfo(ScenarioTextContent data)
        {
            this.data = data;
        }
    }
    public class ScenarioSelectInfo
    {
        public long UniqueId => data.UniqueId;
        public long GroupId => data.GroupId;
        public string SelectText => data.SelectText;
        public SelectType SelectType => data.SelectType;
        public int SelectValue => data.SelectValue;
        public int SelectEnergy => data.SelectEnergy;
        public int SelectVerisimilitude => data.SelectVerisimilitude;

        public long ResultTextContentId => data.ResultTextContentId;
        
        readonly ScenarioSelect data;
        public ScenarioSelectInfo(ScenarioSelect data) { this.data = data;}

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

    public class ScenarioIntroInfo
    {
        public long ChapterId => data.ChapterId;
        public float BackGroundPosX => data.BackGroundPosX;
        public float BackGroundPosY => data.BackGroundPosY;
        public float BackGroundColorR => data.BackGroundColorR;
        public float BackGroundColorG => data.BackGroundColorG;
        public float BackGroundColorB => data.BackGroundColorB;
        public float BackGroundColorA => data.BackGroundColorA;
        public string CharacterClass => data.CharacterClass;
        public string CharacterName => data.CharacterName;

        readonly ScenarioIntro data;

        public ScenarioIntroInfo(ScenarioIntro data)
        {
            this.data = data;
        }
    }

    public class ScenarioData : Database
    {
        public static IDictionary<long, ScenarioWorldInfo> WorldDict = new Dictionary<long, ScenarioWorldInfo>();
        public static IDictionary<long, ScenarioChapterInfo> ChapterDict = new Dictionary<long, ScenarioChapterInfo>();
        public static IDictionary<long,List<ScenarioChapterInfo>> ChapterGroupDict = new Dictionary<long, List<ScenarioChapterInfo>>();
        public static IDictionary<long, ScenarioPageInfo> PageDict = new Dictionary<long, ScenarioPageInfo>();
        public static readonly IDictionary<long, List<ScenarioPageInfo>> PageGroupDict = new Dictionary<long, List<ScenarioPageInfo>>();
        public static readonly IDictionary<long, List<ScenarioPageTextInfo>> PageTextDict = new Dictionary<long, List<ScenarioPageTextInfo>>();
        public static readonly IDictionary<long, List<ScenarioSelectInfo>> SelectGroupDict = new Dictionary<long, List<ScenarioSelectInfo>>();
        public static IDictionary<long, ScenarioDiceInfo> DiceDict = new Dictionary<long, ScenarioDiceInfo>();
        public static IDictionary<long, ScenarioChapterRewardInfo> RewardDict = new Dictionary<long, ScenarioChapterRewardInfo>();
        public static IDictionary<long, ScenarioEndingInfo> EndingDict = new Dictionary<long, ScenarioEndingInfo>();
        public static readonly IDictionary<long, List<ScenarioPageImageInfo>> ImageDict = new Dictionary<long, List<ScenarioPageImageInfo>>();
        public static IDictionary<long,ScenarioIntroInfo> IntroDict = new Dictionary<long, ScenarioIntroInfo>();
        public static IDictionary<long,ScenarioTextContentInfo> ContentDict = new Dictionary<long, ScenarioTextContentInfo>();
        public static IDictionary<long, List<ScenarioTextContentInfo>> ContentGroupDict = new Dictionary<long, List<ScenarioTextContentInfo>>();

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

            ChapterGroupDict.Clear();
            {
                var group = new Data<ScenarioChapter>().GetData(path);

                foreach(var x in group)
                {
                    if (!ChapterGroupDict.ContainsKey(x.WorldId))
                    {
                        ChapterGroupDict.Add(x.WorldId, new());
                    }

                    ChapterGroupDict[x.WorldId] ??= new();

                    ChapterGroupDict[x.WorldId].Add(new ScenarioChapterInfo(x));
                }
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
                    if (!PageTextDict.ContainsKey(x.GroupId))
                    {
                        PageTextDict.Add(x.GroupId, new List<ScenarioPageTextInfo>());
                    }

                    if (PageTextDict[x.GroupId] == null)
                    {
                        PageTextDict[x.GroupId] = new List<ScenarioPageTextInfo>();
                    }
                    PageTextDict[x.GroupId].Add(new ScenarioPageTextInfo(x));
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
                    if (!ImageDict.ContainsKey(group.GroupId))
                    {
                        ImageDict.Add(group.GroupId, new());
                    }

                    ImageDict[group.GroupId] ??= new();

                    ImageDict[group.GroupId].Add(new ScenarioPageImageInfo(group));
                }
            }
            IntroDict.Clear();
            {
                var intro = new Data<ScenarioIntro>().GetData(path);

                IntroDict = intro.ToDictionary(kv => kv.ChapterId, kv => new ScenarioIntroInfo(kv));               
            }
            ContentDict.Clear();
            ContentGroupDict.Clear();
            {
                var content = new Data<ScenarioTextContent>().GetData(path);
                ContentDict = content.ToDictionary(kv => kv.UniqueId, kv => new ScenarioTextContentInfo(kv));
                foreach (var group in content)
                {
                    if (!ContentGroupDict.ContainsKey(group.GroupId))
                    {
                        ContentGroupDict.Add(group.GroupId, new());
                    }

                    ContentGroupDict[group.GroupId] ??= new();

                    ContentGroupDict[group.GroupId].Add(new ScenarioTextContentInfo(group));
                }
            }

        }

        public static bool TryGetTextContent(long uniqueId,out ScenarioTextContentInfo info)
        {
            return ContentDict.TryGetValue(uniqueId, out info);
        }
        public static bool TryGetTextContent(long groupId,out List<ScenarioTextContentInfo> list)
        {
            return ContentGroupDict.TryGetValue(groupId, out list);
        }
        public static bool TryGetWorld(long uniqueId, out ScenarioWorldInfo info)
        {
            return WorldDict.TryGetValue(uniqueId, out info);
        }      
        public static bool TryGetChapter(long uniqueId, out ScenarioChapterInfo info)
        {
            return ChapterDict.TryGetValue(uniqueId, out info);
        }
        public static bool TryGetChapterGroup(long worldId, out List<ScenarioChapterInfo> list)
        {
            return ChapterGroupDict.TryGetValue(worldId, out list);
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
        public static bool TryGetIntro(long chapterId,out  ScenarioIntroInfo info)
        {
            return IntroDict.TryGetValue(chapterId,out info);
        }
    }
}
