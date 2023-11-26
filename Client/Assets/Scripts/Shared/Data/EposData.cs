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

    public class EposData : Database
	{
		public static IDictionary<long, EposLevelInfo> LevelDict = new Dictionary<long, EposLevelInfo>();
		public override void ProcessDataLoad(string path)
		{
			LevelDict.Clear();
			{
				var levels = new Data<EposLevel>().GetData(path);

				LevelDict = levels.ToDictionary(kv => kv.Level, kv => new EposLevelInfo(kv));
			}
		}

		public static bool TryGetEposLevel(long Level, out EposLevelInfo info)
		{
			return LevelDict.TryGetValue(Level, out info);
		}
	}
}