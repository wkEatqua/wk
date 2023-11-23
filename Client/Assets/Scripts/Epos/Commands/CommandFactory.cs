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
            if (tileObject == null)
            {
                Player player = Object.FindAnyObjectByType<Player>();
                return new MoveCommand(player, tile);
            }

            return null;
        }
    }
}