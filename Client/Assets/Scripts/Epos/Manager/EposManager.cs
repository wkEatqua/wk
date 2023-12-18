using Shared.Data;
using Shared.Model;
using System;
using System.Collections;
using System.Collections.Generic;
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
                exp = value;
                while (EposData.TryGetEposMaxExp(level, out EposLevelExpInfo info) && exp >= info.Exp)
                {
                    exp -= info.Exp;
                    level++;
                }
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
        }
    }
}