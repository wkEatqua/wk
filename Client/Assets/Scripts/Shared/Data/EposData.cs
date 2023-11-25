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
	public class EposData : Database
	{
		public static IDictionary<long, EposLevelInfo> LevelDict = new Dictionary<long, EposLevelInfo>();
        static IDictionary<long, EposLevelExpInfo> ExpDict = new Dictionary<long, EposLevelExpInfo>();
		public override void ProcessDataLoad(string path)
		{
			LevelDict.Clear();
			{
				var levels = new Data<EposLevel>().GetData(path);

				LevelDict = levels.ToDictionary(kv => kv.Level, kv => new EposLevelInfo(kv));
			}

            ExpDict.Clear();
            {
                ExpDict = new Data<EposLevelExp>().GetData(path).ToDictionary(kv => kv.Level,kv  => new EposLevelExpInfo(kv));
            }
		}

		public static bool TryGetEposLevel(long Level, out EposLevelInfo info)
		{
			return LevelDict.TryGetValue(Level, out info);
		}
        public static bool TryGetEposExp(long level,out EposLevelExpInfo info)
        {
            return ExpDict.TryGetValue(level, out info);
        }
	}
}