using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using Shared.Model;

namespace Shared.Data
{
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
        public float Level1Tile => data.Level1Tile;
        public float Level2Tile => data.Level2Tile;
        public float Level3Tile => data.Level3Tile;
        public float Level4Tile => data.Level4Tile;
        public float Level5Tile => data.Level5Tile;
        public float Level6Tile => data.Level6Tile;
        public float EmptyTile => data.EmptyTile;
        public float UnmovableTile => data.UnmovableTile;
        public float ObjectTile => data.ObjectTile;
        
        readonly EposTilePercent data;

        public EposTilePercentInfo(EposTilePercent data) { this.data = data; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"PlayerLevel : {PlayerLevel}");
            sb.AppendLine($"Level1Tile : {Level1Tile}");
            sb.AppendLine($"Level2Tile : {Level2Tile}");
            sb.AppendLine($"Level3Tile : {Level3Tile}");
            sb.AppendLine($"Level4Tile : {Level4Tile}");
            sb.AppendLine($"Level5Tile : {Level5Tile}");
            sb.AppendLine($"Level6Tile : {Level6Tile}");
            sb.AppendLine($"EmptyTile : {EmptyTile}");
            sb.AppendLine($"UnmovableTile : {UnmovableTile}");
            sb.AppendLine($"ObjectTile : {ObjectTile}");
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

    public class EposData : Database
	{
		public static IDictionary<long, EposLevelInfo> LevelDict = new Dictionary<long, EposLevelInfo>();
        public static IDictionary<long, EposTilePercentInfo> TilePercentDict = new Dictionary<long, EposTilePercentInfo>();
        public static IDictionary<long, EposObjectPercentInfo> ObjectPercentDict = new Dictionary<long, EposObjectPercentInfo>();
        public static IDictionary<long, EposTileObjectPercentInfo> TileObjectPercentDict = new Dictionary<long, EposTileObjectPercentInfo>();
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