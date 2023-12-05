using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using Shared.Model;

namespace Shared.Data
{
    public class EposInfo
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            var fields = GetType().GetProperties();
            foreach (var field in fields)
            {
                sb.AppendLine($"{field.Name}, {field.GetValue(this)}");
            }

            return sb.ToString();
        }
    }

	public class EposLevelInfo
	{
		public long Level => data.Level;
		public long TileNumber => data.TileNumber;
		public float TileInterval => data.TileInterval;
		public float TileScale => data.TileScale;

		readonly EposLevel data;

		public EposLevelInfo(EposLevel data) { this.data = data; }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine($"Level : {Level}");
			sb.AppendLine($"TileNumber : {TileNumber}");
			sb.AppendLine($"TileInterval : {TileInterval}");
			sb.AppendLine($"TileScale : {TileScale}");
			return sb.ToString();
		}
	}

    public class EposLevelExpInfo
    {
        public long Level => data.Level;
        public int Exp => data.Exp;

        readonly EposLevelExp data;

        public EposLevelExpInfo(EposLevelExp data) { this.data = data; }
    }
    public class EposTileExpInfo
    {
        public TileDifficulty Difficulty => data.Difficulty;
        public int Exp => data.Exp;
        readonly EposTileExp data;
        public EposTileExpInfo(EposTileExp data)
        {
            this.data = data;
        }
    }

    public class EposTileInfo : EposInfo
    {
        public long ID => data.ID;
        public long Lv => data.Lv;
        public long Tile1 => data.Tile1;
        public long TileRate1 => data.TileRate1;
        public long Tile2 => data.Tile2;
        public long TileRate2 => data.TileRate2;
        public long Tile3 => data.Tile3;
        public long TileRate3 => data.TileRate3;
        public long Tile4 => data.Tile4;
        public long TileRate4 => data.TileRate4;
        public long Tile5 => data.Tile5;
        public long TileRate5 => data.TileRate5;
        public long Tile6 => data.Tile6;
        public long TileRate6 => data.TileRate6;
        public long Tile7 => data.Tile7;
        public long TileRate7 => data.TileRate7;
        public long Tile8 => data.Tile8;
        public long TileRate8 => data.TileRate8;
        public long Tile9 => data.Tile9;
        public long TileRate9 => data.TileRate9;

        readonly EposTile data;
        public EposTileInfo(EposTile data) { this.data = data; }
    }
    public class EposTileInfoInfo : EposInfo
    {
        public long GroupID => data.GroupID;
        public string Tile => data.Tile;
        public int Tier => data.Tier;
        public string TileModel => data.TileModel;
        public string Tilepattern => data.Tilepattern;
        public int EXP => data.EXP;

        readonly Shared.Model.EposTileInfo data;
        public EposTileInfoInfo(Shared.Model.EposTileInfo data) { this.data = data; } 
    }

    public class EposTierTileInfo : EposInfo
    {
        public long GroupID => data.GroupID;
        public string Tile => data.Tile;
        public long Object => data.Object;
        public long ObjectRate => data.ObjectRate;

        readonly EposTierTile data;
        public EposTierTileInfo(EposTierTile data) { this.data = data; }
    }
    
    public class EposInteractionTileInfo : EposInfo
    {
        public long GroupID => data.GroupID;
        public string Tile => data.Tile;
        public long Object => data.Object;
        public long ObjectRate => data.ObjectRate;

        readonly EposInteractionTile data;
        public EposInteractionTileInfo(EposInteractionTile data) { this.data = data; }
    }

    public class EposEnvironmentTileInfo : EposInfo
    {
        public long GroupID => data.GroupID;
        public string Tile => data.Tile;
        public long Object => data.Object;
        public long ObjectRate => data.ObjectRate;

        readonly EposEnvironmentTile data;
        public EposEnvironmentTileInfo(EposEnvironmentTile data) { this.data = data; }
    }

    public class EposBlankTileInfo : EposInfo
    {
        public long ID => data.ID;
        public string Tile => data.Tile;
        public long Object => data.Object;
        public long ObjectRate => data.ObjectRate;
        readonly EposBlankTile data;
        public EposBlankTileInfo(EposBlankTile data) { this.data = data; }
    }
    public class EposObjectInfo : EposInfo
    {
        public long ID => data.ID;
        public string Tile => data.Tile;
        public string Type => data.Type;
        public int Tier => data.Tier;
        public string Modeling => data.Modeling;
        public long ProduceInfo => data.ProduceInfo;
        public string RewardType => data.RewardType;
        public long Reward => data.Reward;
        public long Trophy => data.Trophy;

        readonly EposObject data;
        public EposObjectInfo(EposObject data) { this.data = data; }
    }

    public class EposTrophyInfo : EposInfo
    {
        public long ID => data.ID;
        public long TrophyID1 => data.TrophyID1;
        public string Type1 => data.Type1;
        public long Rate1 => data.Rate1;
        public long TrophyID2 => data.TrophyID2;
        public string Type2 => data.Type2;
        public long Rate2 => data.Rate2;
        public long TrophyID3 => data.TrophyID3;
        public string Type3 => data.Type3;
        public long Rate3 => data.Rate3;
        public long TrophyID4 => data.TrophyID4;
        public string Type4 => data.Type4;
        public long Rate4 => data.Rate4;

        readonly EposTrophy data;
        public EposTrophyInfo(EposTrophy data) { this.data = data; }
    }

    public class EposData : Database
	{
		public static IDictionary<long, EposLevelInfo> LevelDict = new Dictionary<long, EposLevelInfo>();
        public static IDictionary<long, EposTileInfo> TileDict = new Dictionary<long, EposTileInfo>();
        public static IDictionary<long, EposTileInfoInfo> TileInfoDict = new Dictionary<long, EposTileInfoInfo>();
        public static readonly IDictionary<long, List<EposTierTileInfo>> TierTileDict = new Dictionary<long, List<EposTierTileInfo>>();
        public static readonly IDictionary<long, List<EposInteractionTileInfo>> InteractionTileDict = new Dictionary<long, List<EposInteractionTileInfo>>();
        public static readonly IDictionary<long, List<EposEnvironmentTileInfo>> EnvironmentTileDict = new Dictionary<long, List<EposEnvironmentTileInfo>>();
        public static readonly IDictionary<long, List<EposBlankTileInfo>> BlankTileDict = new Dictionary<long, List<EposBlankTileInfo>>();
        public static IDictionary<long, EposObjectInfo> ObjectDict = new Dictionary<long, EposObjectInfo>();
        public static IDictionary<long, EposTrophyInfo> TrophyDict = new Dictionary<long, EposTrophyInfo>();


        static IDictionary<long, EposLevelExpInfo> MaxExpDict = new Dictionary<long, EposLevelExpInfo>();
        static IDictionary<TileDifficulty, EposTileExpInfo> TileExpDict = new Dictionary<TileDifficulty, EposTileExpInfo>();

		public override void ProcessDataLoad(string path)
		{
			LevelDict.Clear();
			{
				var levels = new Data<EposLevel>().GetData(path);

				LevelDict = levels.ToDictionary(kv => kv.Level, kv => new EposLevelInfo(kv));
			}

            MaxExpDict.Clear();
            {
                MaxExpDict = new Data<EposLevelExp>().GetData(path).ToDictionary(kv => kv.Level, kv => new EposLevelExpInfo(kv));
            }

            TileExpDict.Clear();
            {
                TileExpDict = new Data<EposTileExp>().GetData(path).ToDictionary(kv => kv.Difficulty, kv => new EposTileExpInfo(kv));
            }

            TileDict.Clear();
            {
                var Tiles = new Data<EposTile>().GetData(path);

                TileDict = Tiles.ToDictionary(kv => kv.Lv, kv => new EposTileInfo(kv));
            }

            TileInfoDict.Clear();
            {
                var TileInfos = new Data<Model.EposTileInfo>().GetData(path);

                TileInfoDict = TileInfos.ToDictionary(kv => kv.GroupID, kv => new EposTileInfoInfo(kv));
            }

            TierTileDict.Clear();
            {
                var TierTiles = new Data<EposTierTile>().GetData(path);

                foreach(var TierTile in TierTiles)
                {
                    if (!TierTileDict.ContainsKey(TierTile.GroupID))
                    {
                        TierTileDict.Add(TierTile.GroupID, new());
                    }

                    TierTileDict[TierTile.GroupID] ??= new();

                    TierTileDict[TierTile.GroupID].Add(new EposTierTileInfo(TierTile));
                }
            }

            InteractionTileDict.Clear();
            {
                var InteractionTiles = new Data<EposInteractionTile>().GetData(path);

                foreach(var InteractionTile in InteractionTiles)
                {
                    if (!InteractionTileDict.ContainsKey(InteractionTile.GroupID))
                    {
                        InteractionTileDict.Add(InteractionTile.GroupID, new());
                    }

                    InteractionTileDict[InteractionTile.GroupID] ??= new();

                    InteractionTileDict[InteractionTile.GroupID].Add(new EposInteractionTileInfo(InteractionTile));
                }
            }

            EnvironmentTileDict.Clear();
            {
                var EnvironmentTiles = new Data<EposEnvironmentTile>().GetData(path);

                foreach (var EnvironmentTile in EnvironmentTiles)
                {
                    if (!EnvironmentTileDict.ContainsKey(EnvironmentTile.GroupID))
                    {
                        EnvironmentTileDict.Add(EnvironmentTile.GroupID, new());
                    }

                    EnvironmentTileDict[EnvironmentTile.GroupID] ??= new();

                    EnvironmentTileDict[EnvironmentTile.GroupID].Add(new EposEnvironmentTileInfo(EnvironmentTile));
                }
            }

            BlankTileDict.Clear();
            {
                var BlankTiles = new Data<EposBlankTile>().GetData(path);

                foreach (var BlankTile in BlankTiles)
                {
                    if (!BlankTileDict.ContainsKey(BlankTile.ID))
                    {
                        BlankTileDict.Add(BlankTile.ID, new());
                    }

                    BlankTileDict[BlankTile.ID] ??= new();

                    BlankTileDict[BlankTile.ID].Add(new EposBlankTileInfo(BlankTile));
                }
            }

            ObjectDict.Clear();
            {
                var Objects = new Data<EposObject>().GetData(path);

                ObjectDict = Objects.ToDictionary(kv => kv.ID, kv => new EposObjectInfo(kv));
            }

            TrophyDict.Clear();
            {
                var Trophys = new Data<EposTrophy>().GetData(path);

                TrophyDict = Trophys.ToDictionary(kv => kv.ID, kv => new EposTrophyInfo(kv));
            }
        }

		public static bool TryGetEposLevel(long Level, out EposLevelInfo Info)
		{
			return LevelDict.TryGetValue(Level, out Info);
		}
        public static bool TryGetEposMaxExp(long level,out EposLevelExpInfo info)
        {
            return MaxExpDict.TryGetValue(level, out info);
        }
        public static bool TryGetEposTileExp(TileDifficulty difficulty,out EposTileExpInfo info)
        {
            return TileExpDict.TryGetValue(difficulty, out info);
        }
        public static bool TryGetEposTile(long Lv, out EposTileInfo Info)
        {
            return TileDict.TryGetValue(Lv, out Info);
        }

        public static bool TryGetEposTileInfo(long GroupID, out EposTileInfoInfo Info)
        {
            return TileInfoDict.TryGetValue(GroupID, out Info);
        }

        public static bool TryGetEposTierTile(long GroupID, out List<EposTierTileInfo> Info)
        {
            return TierTileDict.TryGetValue(GroupID, out Info);
        }

        public static bool TryGetEposInteractionTile(long GroupID, out List<EposInteractionTileInfo> Info)
        {
            return InteractionTileDict.TryGetValue(GroupID, out Info);
        }

        public static bool TryGetEposEnvironmentTile(long GroupID, out List<EposEnvironmentTileInfo> Info)
        {
            return EnvironmentTileDict.TryGetValue(GroupID, out Info);
        }

        public static bool TryGetEposBlankTile(long GroupID, out List<EposBlankTileInfo> Info)
        {
            return BlankTileDict.TryGetValue(GroupID, out Info);
        }

        public static bool TryGetEposObject(long ID, out EposObjectInfo Info)
        {
            return ObjectDict.TryGetValue(ID, out Info);
        }

        public static bool TryGetEposTrophy(long ID, out EposTrophyInfo Info)
        {
            return TrophyDict.TryGetValue(ID, out Info);
        }
    }
}