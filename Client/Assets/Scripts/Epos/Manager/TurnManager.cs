using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Epos
{
    public class TurnManager : Singleton<TurnManager>
    {
        public delegate IEnumerator EventHandler();
        int turnCount;
        public int TurnCount => turnCount;

        EventHandler onPlayerTurnStart;
        EventHandler onPlayerTurnEnd;
        EventHandler onEnemyTurnStart;
        EventHandler onEnemyTurnEnd;


        public event EventHandler OnPlayerTurnStart
        {
            add
            {
                if(onPlayerTurnStart == null || !onPlayerTurnStart.GetInvocationList().Contains(value))
                {
                    onPlayerTurnStart += value;
                }
            }
            remove
            {
                onPlayerTurnStart -= value;
            }
        }
        public event EventHandler OnPlayerTurnEnd
        {
            add
            {
                if (onPlayerTurnEnd == null || !onPlayerTurnEnd.GetInvocationList().Contains(value))
                {
                    onPlayerTurnEnd += value;
                }
            }
            remove
            {
                onPlayerTurnEnd -= value;
            }
        }
        public event EventHandler OnEnemyTurnStart
        {
            add
            {
                if (onEnemyTurnStart == null || !onEnemyTurnStart.GetInvocationList().Contains(value))
                {
                    onEnemyTurnStart += value;
                }
            }
            remove
            {
                onEnemyTurnStart -= value;
            }
        }
        public event EventHandler OnEnemyTurnEnd
        {
            add
            {
                if (onEnemyTurnEnd == null || !onEnemyTurnEnd.GetInvocationList().Contains(value))
                {
                    onEnemyTurnEnd += value;
                }
            }
            remove
            {
                onEnemyTurnEnd -= value;
            }
        }

        enum TurnType { Player,Enemy}
        TurnType turnType;


        private void Start()
        {
            OnEnemyTurnStart += TileManager.Instance.RemoveAndCreateGrace;
        }
        protected override void Awake()
        {
            base.Awake();
            turnCount = 0;           
            
            OnPlayerTurnStart += AddTurn;
            turnType = TurnType.Player;           
        }
        IEnumerator AddTurn()
        {
            turnCount++;
            yield break;
        }
               
        IEnumerator InvokeEvent(EventHandler eventHandler)
        {
            if (eventHandler == null) yield break;

            foreach(var coroutine in eventHandler.GetInvocationList())
            {
                if (coroutine != null)
                {
                    yield return StartCoroutine((coroutine as EventHandler).Invoke());
                }
            }
        }
        public void StartTurn()
        {
            StartCoroutine(StartTurnCoroutine());
        }
        public void EndTurn()
        {
            StartCoroutine(EndTurnCoroutine());
        }
        IEnumerator StartTurnCoroutine()
        {   
            switch(turnType)
            {
                case TurnType.Player:
                    yield return StartCoroutine(InvokeEvent(onPlayerTurnStart));
                    break;
                case TurnType.Enemy:
                    yield return StartCoroutine(InvokeEvent(onEnemyTurnStart));
                    EndTurn();
                    break;
            }
        }
        
        IEnumerator EndTurnCoroutine()
        {
            switch(turnType)
            {
                case TurnType.Player:
                    yield return StartCoroutine(InvokeEvent(onPlayerTurnEnd));
                    turnType = TurnType.Enemy;
                    break;
                case TurnType.Enemy:
                    yield return StartCoroutine(InvokeEvent(onEnemyTurnEnd));
                    turnType = TurnType.Player;
                    break;
            }
            StartTurn();
        }
    }
}