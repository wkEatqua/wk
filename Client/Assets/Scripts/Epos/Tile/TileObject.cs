using DG.Tweening;
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

        public virtual IEnumerator MoveTo(int x,int y)
        {
            var tileMap = TileManager.Instance.TileMap;
            int removePosX = tile.X;
            int removePosY = tile.Y;
            
            yield return transform.DOMove(tileMap[x][y].transform.position, 0.5f).WaitForCompletion();

            tileMap[x][y].SetObject(this);
            tileMap[removePosX][removePosY].SetObject(null);

            yield return new WaitForEndOfFrame();

        }
    }
}