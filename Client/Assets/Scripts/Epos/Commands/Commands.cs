using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Epos
{
    public class MoveCommand : ICommand
    {
        readonly Player player;
        readonly Tile tile;
        public MoveCommand(Player player, Tile tile)
        {
            this.player = player;
            this.tile = tile;   
        }
        public IEnumerator Excute()
        {
            tile.Selector.OnConfirmed.Invoke();
            int graceX = player.tile.X;
            int graceY = player.tile.Y;
            TileManager.Instance.playerLastPos = (graceX, graceY);
            yield return player.MoveTo(tile.X, tile.Y);
            TileManager.Instance.SetPlayerPos(tile.X, tile.Y);
            EposManager.Instance.OnMove.Invoke(tile);
            TileManager.Instance.graceTiles.Enqueue((graceX, graceY));           
            TurnManager.Instance.EndTurn();
        }
    }

    public class CollectCommand : ICommand
    {
        readonly Player player;
        readonly Tile tile;
        public CollectCommand(Player player, Tile tile)
        {
            this.player = player;
            this.tile = tile;
        }
        public IEnumerator Excute()
        {
            tile.Selector.OnConfirmed.Invoke();
            int graceX = player.tile.X;
            int graceY = player.tile.Y;
            TileManager.Instance.playerLastPos = (graceX, graceY);
            TileObject obj = tile.Selector.Obj;
            yield return player.MoveTo(tile.X, tile.Y);
            TileManager.Instance.SetPlayerPos(tile.X, tile.Y);
            EposManager.Instance.OnMove.Invoke(tile);
            TileManager.Instance.graceTiles.Enqueue((graceX, graceY));
            if(obj != null)
            {
                obj.Collect();
            }
        }
    }
    public class InteractCommand : ICommand
    {
        readonly Player player;
        readonly Tile tile;

        public InteractCommand(Player player, Tile tile)
        {
            this.player = player;
            this.tile = tile;
        }

        public IEnumerator Excute()
        {
            if(tile.Selector.Obj != null)
            {
                tile.Selector.Obj.Collect();
            }
            yield return null;
        }
    }
    public class AttackCommand : ICommand
    {
        readonly Player player;
        readonly Monster target;

        public AttackCommand(Player player, Monster target)
        {
            this.player = player;
            this.target = target;
        }

        public IEnumerator Excute()
        {
            if(target != null)
            {
                target.tile.Selector.OnConfirmed.Invoke();
                player.Attack(target);
                yield return new WaitForSeconds(1f);
                TurnManager.Instance.EndTurn();
            }
        }
    }  
}