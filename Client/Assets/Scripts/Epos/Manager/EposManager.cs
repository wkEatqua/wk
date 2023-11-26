using Shared.Data;
using Shared.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Epos
{
    public class EposManager : Singleton<EposManager>
    {
        [HideInInspector] public int gold;
        [HideInInspector] public int moveCount;
        [HideInInspector] public int eventCount;
        [HideInInspector] public int level;
        [HideInInspector] public int itemCount;

        int exp;

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
        public Player Player => player;

        protected override void Awake()
        {
            base.Awake();

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

            player = FindAnyObjectByType<Player>();
        }

        private void Start()
        {
           
        }
    }
}