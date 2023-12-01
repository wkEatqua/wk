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
	
    public class EposTilePercentInfo
    {
        public long PlayerLevel => data.PlayerLevel;
        public float Tier1 => data.Tier1;
        public float Tier2 => data.Tier2;
        public float Tier3 => data.Tier3;
        public float Tier4 => data.Tier4;
        public float Tier5 => data.Tier5;
        public float Tier6 => data.Tier6;
        public float Empty => data.Empty;
        public float Unmovable => data.Unmovable;
        public float Object => data.Object;
        
        readonly EposTilePercent data;

        public EposTilePercentInfo(EposTilePercent data) { this.data = data; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"PlayerLevel : {PlayerLevel}");
            sb.AppendLine($"Tier1 : {Tier1}");
            sb.AppendLine($"Tier2 : {Tier2}");
            sb.AppendLine($"Tier3 : {Tier3}");
            sb.AppendLine($"Tier4 : {Tier4}");
            sb.AppendLine($"Tier5 : {Tier5}");
            sb.AppendLine($"Tier6 : {Tier6}");
            sb.AppendLine($"Empty : {Empty}");
            sb.AppendLine($"Unmovable : {Unmovable}");
            sb.AppendLine($"Object : {Object}");
            return sb.ToString();
        }
    }

    public class EposObjectPercentInfo
    {
        public long PlayerLevel => data.PlayerLevel;
        public float SmallHPRecover => data.SmallHPRecover;
        public float LargeHPRecover => data.LargeHPRecover;
        public float Event => data.Event;

        readonly EposObjectPercent data;
        public EposObjectPercentInfo(EposObjectPercent data) { this.data = data; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"PlayerLevel : {PlayerLevel}");
            sb.AppendLine($"SmallHPRecover : {SmallHPRecover}");
            sb.AppendLine($"LargeHPRecover : {LargeHPRecover}");
            sb.AppendLine($"Event : {Event}");
            return sb.ToString();
        }
    }

    public class EposTileObjectPercentInfo
    {
        public long TileTier => data.TileTier;
        public float SmallHPRecover => data.SmallHPRecover;
        public float LargeHPRecover => data.LargeHPRecover;
        public float Level1Monster => data.Level1Monster;
        public float Level2Monster => data.Level2Monster;
        public float Level3Monster => data.Level3Monster;
        public float Level4Monster => data.Level4Monster;
        public float Level5Monster => data.Level5Monster;
        public float Level6Monster => data.Level6Monster;
        public float Weapon => data.Weapon;
        public float Armor => data.Armor;
        public float SmallGold => data.SmallGold;
        public float LargeGold => data.LargeGold;
        public float Card => data.Card;

        readonly EposTileObjectPercent data;
        public EposTileObjectPercentInfo(EposTileObjectPercent data) { this.data = data; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"TileTier : {TileTier}");
            sb.AppendLine($"SmallHPRecover : {SmallHPRecover}");
            sb.AppendLine($"LargeHPRecover : {LargeHPRecover}");
            sb.AppendLine($"Level1Monster : {Level1Monster}");
            sb.AppendLine($"Level2Monster : {Level2Monster}");
            sb.AppendLine($"Level3Monster : {Level3Monster}");
            sb.AppendLine($"Level4Monster : {Level4Monster}");
            sb.AppendLine($"Level5Monster : {Level5Monster}");
            sb.AppendLine($"Level6Monster : {Level6Monster}");
            sb.AppendLine($"Weapon : {Weapon}");
            sb.AppendLine($"Armor : {Armor}");
            sb.AppendLine($"SmallGold : {SmallGold}");
            sb.AppendLine($"LargeGold : {LargeGold}");
            sb.AppendLine($"Card : {Card}");
            return sb.ToString();
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

    public class EposData : Database
	{
		public static IDictionary<long, EposLevelInfo> LevelDict = new Dictionary<long, EposLevelInfo>();
        public static IDictionary<long, EposTilePercentInfo> TilePercentDict = new Dictionary<long, EposTilePercentInfo>();
        public static IDictionary<long, EposObjectPercentInfo> ObjectPercentDict = new Dictionary<long, EposObjectPercentInfo>();
        public static IDictionary<long, EposTileObjectPercentInfo> TileObjectPercentDict = new Dictionary<long, EposTileObjectPercentInfo>();
        public static IDictionary<long, EposTileInfo> TileDict = new Dictionary<long, EposTileInfo>();
        public static IDictionary<long, EposTileInfoInfo> TileInfoDict = new Dictionary<long, EposTileInfoInfo>();
        public static IDictionary<long, EposTierTileInfo> TierTileDict = new Dictionary<long, EposTierTileInfo>();
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

            TilePercentDict.Clear();
            {
                var Percents = new Data<EposTilePercent>().GetData(path);

                TilePercentDict = Percents.ToDictionary(kv => kv.PlayerLevel, kv => new EposTilePercentInfo(kv));
            }

            ObjectPercentDict.Clear();
            {
                var ObjectPercents = new Data<EposObjectPercent>().GetData(path);

                ObjectPercentDict = ObjectPercents.ToDictionary(kv => kv.PlayerLevel, kv => new EposObjectPercentInfo(kv));
            }

            TileObjectPercentDict.Clear();
            {
                var TileObjectPercents = new Data<EposTileObjectPercent>().GetData(path);

                TileObjectPercentDict = TileObjectPercents.ToDictionary(kv => kv.TileTier, kv => new EposTileObjectPercentInfo(kv));
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

                TierTileDict = TierTiles.ToDictionary(kv => kv.GroupID, kv => new EposTierTileInfo(kv));
            }
		}

		public static bool TryGetEposLevel(long Level, out EposLevelInfo Info)
		{
			return LevelDict.TryGetValue(Level, out Info);
		}

        public static bool TryGetEposTilePercent(long PlayerLevel, out EposTilePercentInfo Info)
        {
            return TilePercentDict.TryGetValue(PlayerLevel, out Info);
        }

        public static bool TryGetEposObjectPercent(long PlayerLevel, out EposObjectPercentInfo Info)
        {
            return ObjectPercentDict.TryGetValue(PlayerLevel, out Info);
        }
        public static bool TryGetEposTileObjectPercent(long TileTier, out EposTileObjectPercentInfo Info)
        {
            return TileObjectPercentDict.TryGetValue(TileTier, out Info);
        }

        public static bool TryGetEposTile(long Lv, out EposTileInfo Info)
        {
            return TileDict.TryGetValue(Lv, out Info);
        }

        public static bool TryGetEposMaxExp(long level,out EposLevelExpInfo info)
        {
            return MaxExpDict.TryGetValue(level, out info);
        }
        public static bool TryGetEposTileExp(TileDifficulty difficulty,out EposTileExpInfo info)
        {
            return TileExpDict.TryGetValue(difficulty, out info);
        }
	}
}