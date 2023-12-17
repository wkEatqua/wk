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

            if ((tileObject == null || tileObject is ItemObject) && tile.Type != Tile.TileType.Grace)
            {
                return new MoveCommand(player, tile);
            }
            else if(tileObject is Monster monster)
            {
                return new AttackCommand(player, monster);
            }
            else if(tileObject is InteractableObject interact)
            {
                return new InteractCommand(interact);
            }
            
            return null;
        }
    }
}