using Shared.Data;
using Shared.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Epos
{
    public class EposManager : Singleton<EposManager>
    {
        public UnityEvent OnGameReset = new();
        int gold;
        public int moveCount;
        public int eventCount;
        public int level;
        public int itemCount;

        int maxLevel;

        int exp;
        public int Gold
        {
            get
            {
                return gold;
            }
            set
            {
                int gain = value - gold;
                gain *= 100 + player.GoldGain;
                gain /= 100;

                gold += gain;
            }
        }
        public int Exp
        {
            get
            {
                return exp;
            }
            set
            {
                if (level >= maxLevel) return;

                exp = value;
                EposLevelExpInfo info;
                while (EposData.TryGetEposMaxExp(level, out info) && exp >= info.Exp)
                {
                    exp -= info.Exp;
                    level++;
                }

                if (info == null)
                {
                    level = maxLevel;
                    exp = 0;
                }
            }
        }
        public int MaxExp
        {
            get
            {
                EposData.TryGetEposMaxExp(level, out EposLevelExpInfo info);
                return info?.Exp ?? 0;
            }
        }
        [HideInInspector] public UnityEvent<Tile> OnMove = new();
        Player player;
        public Player Player
        {
            get
            {
                return player;
            }
            set
            {
                player = value;
            }
        }
        protected override void Awake()
        {
            base.Awake();
            OnMove.AddListener(tile => moveCount++);
            OnMove.AddListener(tile =>
            {
                int value = 0;

                if (tile.Difficulty != TileDifficulty.None)
                {
                    EposData.TryGetEposTileExp(tile.Difficulty, out EposTileExpInfo info);
                    value = info.Exp;
                }
                Exp += value;
            });
            OnGameReset.AddListener(() =>
            {
                CardManager.CardPatternCounts.Clear();

                foreach (CardPattern pattern in Enum.GetValues(typeof(CardPattern)))
                {
                    CardManager.CardPatternCounts.Add(pattern, 0);
                }
            });
        }

        private void Start()
        {
            OnGameReset.Invoke();
            maxLevel = (int)EposData.LevelDict.Keys.Max(x => x) + 1;
        }
    }
}