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
            yield return player.transform.DOMove(tile.transform.position, 1f).WaitForCompletion();

            TileManager.Instance.graceTiles.Enqueue((player.tile.X, player.tile.Y));
            player.tile.SetObject(null);
            tile.SetObject(player);
        }
    }
}