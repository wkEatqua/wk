using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Epos
{
    public abstract class TileObject : MonoBehaviour
    {
        public enum ObjectType
        {
            Player, Monster, Treasure, Obstacle
        }

        public abstract ObjectType Type { get; }

        public Tile tile;
    }
}