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

            yield return player.MoveTo(tile.X, tile.Y);

            TileManager.Instance.graceTiles.Enqueue((graceX, graceY));           
            TurnManager.Instance.EndTurn();
        }
    }
}