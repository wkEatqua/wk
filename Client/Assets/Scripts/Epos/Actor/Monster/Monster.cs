using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos
{
    public abstract class Monster : Actor
    {
        public override ObjectType Type => ObjectType.Monster;     

        private void OnEnable()
        {
            TurnManager.Instance.OnEnemyTurnStart -= UseTurn;
            TurnManager.Instance.OnEnemyTurnStart += UseTurn;
        }
        protected abstract IEnumerator UseTurn();       

        protected virtual void OnDisable()
        {
            TurnManager.Instance.OnEnemyTurnStart -= UseTurn;
        }
    }
}