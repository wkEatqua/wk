using Epos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Epos
{
    public class CommandFactory
    {
        public static ICommand CreateCommand(Tile tile, TileObject tileObject)
        {
            Player player = Object.FindAnyObjectByType<Player>();

            if (tileObject == null && tile.Type == Tile.TileType.Normal)
            {
                return new MoveCommand(player, tile);
            }
            else if(tileObject is Monster monster)
            {
                return new AttackCommand(player, monster);
            }
            return null;
        }
    }
}