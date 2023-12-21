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
            Player player = EposManager.Instance.Player;
            if (tile.Type == Tile.TileType.Grace) return null;

            if (tileObject == null)
            {
                return new MoveCommand(player, tile);
            }
            else if (tileObject is ItemObject)
            {
                return new CollectCommand(player, tile);
            }
            else if (tileObject is InteractableObject)
            {
                return new InteractCommand(player, tile);
            }
            else if(tileObject is Monster monster)
            {
                return new AttackCommand(player, monster);
            }           
            
            return null;
        }
    }
}