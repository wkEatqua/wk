using Shared.Data;
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
                while (EposData.TryGetEposExp(level, out EposLevelExpInfo info) && exp >= info.Exp)
                {
                    exp -= info.Exp;
                    level++;
                }
            }
        }

        [HideInInspector] public UnityEvent<Tile> OnMove = new();

        protected override void Awake()
        {
            base.Awake();

            OnMove.AddListener(tile => Exp += 10);
        }
    }
}